using System.Diagnostics;
using FluentValidation;
using WebApplication1.DTOs;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Service.Impl.UserMagement.Strategy;
using WebApplication1.Service.Interface;
using WebApplication1.Service.Interface.UserMangement;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginDTos> _loginValidator;
    private readonly ProfileStrategyContext _strategyContext;

    public AuthService(IUnitOfWork unitOfWork, IPasswordService passwordService, ITokenService tokenService, IValidator<LoginDTos> loginValidator, ProfileStrategyContext strategyContext)
    {
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _loginValidator = loginValidator;
        _strategyContext = strategyContext;
    }
    
    
    public async Task<Result<LoginUserResponse>> UserLogin(LoginDTos request,CancellationToken cancellationToken)
    {
        var validationResult = await _loginValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<LoginUserResponse>.Failure(
                Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
        }
        
        
        var exactUser = await _unitOfWork.Users.GetUserByEmail(request.Email, cancellationToken);

        if (exactUser is null)
        {
            return Result<LoginUserResponse>.Failure(
                Error.Validation("Auth.Invalid", "The email or password is not correct.")
            );
        }



        if (exactUser.AccountStatus == AccountStatus.LocKed)
        {
            return Result<LoginUserResponse>.Failure(
                Error.Failure("Account.Suspended", "Your account has been suspended.")
            );
        }

        var isPasswordValid = _passwordService.PasswordVerify(request.Password, exactUser.password);

        if (!isPasswordValid)
        {
            return Result<LoginUserResponse>.Failure(
                Error.Validation("Auth.Invalid", "The email or password is not correct.")
            );
        }

        var token = _tokenService.GenerateToken(
            exactUser.id.ToString(),
            exactUser.Role.ToString()
        );

        var response = new LoginUserResponse(
            exactUser.IsEmailConfirmed,
            exactUser.AccountStatus,
            token
        );

        return Result<LoginUserResponse>.Success(response);
    }
    
    
    public async Task<Result<object>> GetMe(Guid userId,CancellationToken ct)
    {
        var strategyResult = await _strategyContext.GetStrategy(userId);

        if (strategyResult.IsFailure)
        {
            return Result<object>.Failure(strategyResult.Error.FirstOrDefault());
        }

        Debug.Assert(strategyResult.Value != null, "strategyResult.Value != null");
        var result = await strategyResult.Value.GetProfileAsync(userId, ct);

        return result;
    }
}
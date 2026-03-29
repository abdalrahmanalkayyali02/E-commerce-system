using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.userMangement.User.Login.Command;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Feature.userMangement.User.Login.Handler
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService _tokenService;
        private readonly IPasswordService _passwordService;

        public LoginUserCommandHandler(
            IUnitOfWork unitOfWork,
            IJWTService tokenService,
            IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand command, CancellationToken ct)
        {
            //var userEmailSpec = new UserByEmailSpecification(command.email);
            var exactUser = await _unitOfWork.Users.GetUserByEmail(command.email, ct);

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

            var isPasswordValid = _passwordService.PasswordVerify(command.password, exactUser.Password.Value);

            if (!isPasswordValid)
            {
                return Result<LoginUserResponse>.Failure(
                    Error.Validation("Auth.Invalid", "The email or password is not correct.")
                );
            }

            var token = _tokenService.GenerateToken(
                exactUser.Id.ToString(),
                exactUser.userType.ToString()
            );

            var response = new LoginUserResponse(
                token,
                exactUser.IsEmailConfirmed,
                exactUser.AccountStatus
            );

            return Result<LoginUserResponse>.Success(response);
        }
    }
}
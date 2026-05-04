using FluentValidation;
using WebApplication1.Data.Model;
using WebApplication1.DTOs;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Service.Interface.UserMangement;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Helper;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement;

public class VerificationService : IVerificationService
{
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateForgetPasswordDtosRequest> _validator;

    public VerificationService(
        IEmailService emailService, 
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IPasswordService passwordService,
        IValidator<UpdateForgetPasswordDtosRequest> validator)
    {
        _emailService = emailService;
        _tokenService = tokenService;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    
    public async Task<Result<string>> ResendOtpService(string email, OtpType otpType, CancellationToken cancellation)
    {
        var existUser = await _unitOfWork.Users.GetUserByEmail(email, cancellation);

        if (existUser is null)
        {
            return Result<string>.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        // Generate raw OTP string
        var generatedOtp = OTP.Generate();
        
        var userOtp = CreateOtpModel(existUser.id, generatedOtp.Value, otpType);

        await _unitOfWork.UserOTp.AddAsync(userOtp, cancellation);
        await _emailService.SendOtpEmailAsync(existUser.Email, generatedOtp.Value, otpType);
        await _unitOfWork.SaveChangesAsync(cancellation);

        return Result<string>.Success("OTP has been resent successfully.");
    }

    public async Task<Result<VerifiedOtpResponse>> VerifiedOtpService(string email, string otp, OtpType type, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetUserByEmail(email, cancellationToken);

        if (user is null)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        var latestOtp = await _unitOfWork.UserOTp.GetLastOtpOfType(user.id, type, cancellationToken);

        // Validation: Match raw strings since VOs are removed
        if (latestOtp is null || latestOtp.Code != otp)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.Validation("OTP.Invalid", "Invalid OTP code."));
        }

        // DateTime.Now used for Npgsql LegacyTimestamp compatibility
        if (latestOtp.ExpiresAt < DateTime.Now)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.Validation("OTP.Expired", "OTP has expired."));
        }

        if (latestOtp.IsUsed || latestOtp.IsVerified)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.Validation("OTP.Used", "OTP already processed."));
        }

        latestOtp.IsVerified = true;
        latestOtp.IsUsed = true;
        latestOtp.TimeVerfied = DateTime.Now;
        latestOtp.UpdateAt = DateTime.Now;

        _unitOfWork.UserOTp.Update(latestOtp, cancellationToken);

        // Update anemic model properties directly in the service
        switch (type)
        {
            case OtpType.registration:
                user.IsEmailConfirmed = true; 
                user.AccountStatus = AccountStatus.Active;
                break;

            case OtpType.forgotPassword:
                // Set the 10-minute window for password update
                user.ResetPasswordAllowedUntil = DateTime.Now.AddMinutes(10);
                break;

            default:
                return Result<VerifiedOtpResponse>.Failure(
                    Error.Validation("OTP.Type", $"Unsupported OTP action: {type}"));
        }

        _unitOfWork.Users.Update(user, cancellationToken);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var token = _tokenService.GenerateToken(user.id.ToString(), user.Role.ToString());

            return Result<VerifiedOtpResponse>.Success(
                new VerifiedOtpResponse(
                    "Verification successful.",
                    user.IsEmailConfirmed,
                    user.AccountStatus,
                    token
                ));
        }
        catch (Exception ex)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.Unexpected("Database.Error", ex.Message));
        }
    }

    public async Task<Result<UpdateForgetPasswordDtOsResponse>> UpdateForgetPassword(UpdateForgetPasswordDtosRequest request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return Result<UpdateForgetPasswordDtOsResponse>.Failure(
                Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
        }
        
        var userEntity = await _unitOfWork.Users.GetUserByEmail(request.Email, ct);
        if (userEntity is null)
        {
            return Result<UpdateForgetPasswordDtOsResponse>.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        // Logic moved from the Entity to the Service layer
        if (userEntity.ResetPasswordAllowedUntil == null || DateTime.Now > userEntity.ResetPasswordAllowedUntil)
        {
            return Result<UpdateForgetPasswordDtOsResponse>.Failure(
                Error.Validation("Reset.WindowExpired", "The password reset window has expired."));
        }

        // Check if new password matches old password using PasswordService
        if (_passwordService.PasswordVerify(request.Password, userEntity.password))
        {
            return Result<UpdateForgetPasswordDtOsResponse>.Failure(
                Error.Validation("Reset.SamePassword", "New password cannot be the same as the current password."));
        }

        // Update anemic entity properties
        userEntity.password = _passwordService.PasswordHash(request.Password);
        userEntity.ResetPasswordAllowedUntil = null; 
        userEntity.UpdateAt = DateTime.Now;

        _unitOfWork.Users.Update(userEntity, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        var token = _tokenService.GenerateToken(userEntity.id.ToString(), userEntity.Role.ToString());

        return Result<UpdateForgetPasswordDtOsResponse>.Success(new UpdateForgetPasswordDtOsResponse(
            "Password updated successfully.",
            token,
            userEntity.IsEmailConfirmed,
            userEntity.AccountStatus));
    }

    private UserOtpDataModel CreateOtpModel(Guid userId, string code, OtpType type) => new()
    {
        ID = Guid.CreateVersion7(), 
        userID = userId, 
        Code = code, 
        OTPtype = type, 
        IsUsed = false,
        IsVerified = false,
        GeneratedAt = DateTime.Now,
        UpdateAt = DateTime.Now,
        ExpiresAt = DateTime.Now.AddMinutes(10)
    };
}
using WebApplication1.Data.Model;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public VerificationService(IEmailService emailService, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    
    public async Task<Result<string>> ResendOtpService(string email, OtpType otpType, CancellationToken cancellation)
    {
        var existUser = await _unitOfWork.Users.GetUserByEmail(email, cancellation);

        if (existUser is null)
        {
            return Result<string>.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        // Generate new OTP Value Object
        var generatedOtp = OTP.Generate();
        
        // Use the passed otpType to ensure the database record is categorized correctly
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

        if (latestOtp is null || latestOtp.Code != otp)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.Validation("OTP.Invalid", "Invalid OTP code."));
        }

        if (latestOtp.ExpiresAt < DateTime.Now)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.Validation("OTP.Expired", "OTP has expired."));
        }

        if (latestOtp.IsUsed || latestOtp.IsVerified)
        {
            return Result<VerifiedOtpResponse>.Failure(Error.Validation("OTP.Used", "OTP has already been processed."));
        }

        // Update OTP state to prevent further use
        latestOtp.IsVerified = true;
        latestOtp.IsUsed = true;
        latestOtp.TimeVerfied = DateTime.Now;
        latestOtp.UpdateAt = DateTime.Now;

        _unitOfWork.UserOTp.Update(latestOtp, cancellationToken);

        // Apply domain logic based on the OTP type
        switch (type)
        {
            case OtpType.registration:
                user.IsEmailConfirmed = true; 
                user.AccountStatus = AccountStatus.Active;
                break;

            case OtpType.forgotPassword:
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

            // Return a JWT so the user is authenticated immediately after verification
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
    
    // Helper to centralize model creation with Guid V7 for sortable primary keys
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
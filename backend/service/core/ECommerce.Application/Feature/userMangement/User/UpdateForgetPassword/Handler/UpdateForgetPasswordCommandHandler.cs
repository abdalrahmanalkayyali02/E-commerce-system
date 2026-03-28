using Common.DTOs.IAC.User;
using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.userMangement.ApplicationError;
using ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Handler
{
    public class UpdateForgetPasswordCommandHandler : IRequestHandler<updateForgetPasswordCommand, Result<UpdateForgetPasswordReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IJWTService _jwtService;
        private readonly IValidator<updateForgetPasswordCommand> _validator;

        public UpdateForgetPasswordCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordService passwordService,
            IJWTService jwtService,
            IValidator<updateForgetPasswordCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _validator = validator;
        }

        public async Task<Result<UpdateForgetPasswordReponse>> Handle(updateForgetPasswordCommand command, CancellationToken ct)
        {
            // 1. Fluent Validation Check
            var validationResult = await _validator.ValidateAsync(command, ct);
            if (!validationResult.IsValid)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(
                    Error.Validation("Input.Invalid", validationResult.Errors.First().ErrorMessage));
            }

            // 2. Identify User
            var userEntity = await _unitOfWork.Users.GetUserByEmail(command.email, ct);
            if (userEntity is null)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(EmailAppError.NotFound);
            }

            // 3. Security Check: The 10-Minute Sliding Window
            // Pass userEntity.Id to ensure we get the OTP for THIS specific user
            var lastOtp = await _unitOfWork.UserOTp.GetLastOtpOfType(userEntity.Id, OtpType.forgotPassword, ct);

            if (lastOtp == null || !lastOtp.IsVerified)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(
                    Error.Validation("OTP.Required", "You must verify your OTP before resetting your password."));
            }

    
            var criticalTime = lastOtp.TimeVerfied!.Value.AddMinutes(10);

            if (DateTime.UtcNow > criticalTime)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(
                    Error.Validation("OTP.WindowExpired", "The 10-minute window since verification has ended. Please request a new OTP.")
                );
            }

            var passwordVo = Password.From(command.password);
            if (passwordVo.IsError)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(passwordVo.Errors);
            }

            string hashedPassword = _passwordService.PasswordHash(passwordVo.Value.Value);

            userEntity.CompletePasswordReset(Password.Reconstruct(hashedPassword));

            _unitOfWork.Users.Update(userEntity);

            var saveResult = await _unitOfWork.SaveChangesAsync(ct);

            if (saveResult <= 0)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(
                    Error.Failure("Database.Error", "Failed to update password. Please try again."));
            }

            var token = _jwtService.GenerateToken(userEntity.Id.ToString(), userEntity.userType.ToString());

            return Result<UpdateForgetPasswordReponse>.Success(new UpdateForgetPasswordReponse(
                "Password updated successfully.",
                token,
                userEntity.IsEmailConfirmed,
                userEntity.AccountStatus));
        }
    }
}
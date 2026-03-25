using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Enum;
using Common.Impl.Result;
using Common.DTOs.IAC.OTpVerfrication;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.IAC.ApplicationError;
using ECommerce.Application.Feature.IAC.OtpVerification.Verified.Command;
using ECommerce.Domain.modules.IAC.Specfication;
using ECommerce.Domain.Modules.IAC.DomainError;
using Common.Result;

namespace ECommerce.Application.Feature.IAC.OtpVerification.Verified.Handler
{
    public class VerfiedOtpCommandHandler : IRequestHandler<VerfiedOtpCommand, Result<VerfiedOtpResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerfiedOtpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VerfiedOtpResponse>> Handle(VerfiedOtpCommand request, CancellationToken cancellationToken)
        {
            // 1. Find the User by Email
            var emailSpec = new UserByEmailSpecification(request.email);
            var user = await _unitOfWork.Users.GetEntityWithSpec(emailSpec, cancellationToken);

            if (user is null)
            {
                return Result<VerfiedOtpResponse>.Failure(EmailAppError.NotFound);
            }

            // 2. Find the Latest OTP based on UserId and the specific Type
            var otpSpec = new LastActiveOtpByUserIdSpecfication(user.Id, request.type);
            var latestOtp = await _unitOfWork.UserOTp.GetEntityWithSpec(otpSpec, cancellationToken);

            // 3. Security & Validation Checks
            if (latestOtp is null || latestOtp.Code.Value != request.otp)
            {
                // Logic: If OTP exists but code is wrong, you should normally increment FailedAttempts here
                return Result<VerfiedOtpResponse>.Failure(OtpAppErrors.InvalidCode);
            }

            if (latestOtp.IsExpired())
            {
                return Result<VerfiedOtpResponse>.Failure(OtpDomainErrors.Expired);
            }

            if (latestOtp.IsUsed)
            {
                return Result<VerfiedOtpResponse>.Failure(OtpDomainErrors.AlreadyUsed);
            }

            try
            {
                // 4. Mark OTP as Used so it can't be reused
                latestOtp.MarkAsVerfied();
                _unitOfWork.UserOTp.Update(latestOtp);


                switch (request.type)
                {
                    case OtpType.registration:
                        user.VerifyAccount();
                        break;

                    case OtpType.forgotPassword:
                        user.AllowPasswordReset();
                        break;

                    default:
                        // This block triggers if the Enum value exists but isn't handled here
                        return Result<VerfiedOtpResponse>.Failure(
                            Error.Validation("OTP.Type", $"Unsupported OTP action: {request.type}"));
                }

                // 6. Persist User changes (e.g., AccountStatus changed to Active)
                _unitOfWork.Users.Update(user);

                // 7. Save all changes to the database in one transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<VerfiedOtpResponse>.Success(
                    new VerfiedOtpResponse(
                        "Verification successful.",
                        user.IsEmailConfirmed,
                        user.AccountStatus,
                        "test-token-placeholder" // You will replace this with real JWT later
                    ));
            }
            catch (Exception)
            {
                // Log the exception here if you have a logger
                return Result<VerfiedOtpResponse>.Failure(
                    Error.Failure("Server.Error", "An error occurred while updating the database."));
            }
        }
    }
}
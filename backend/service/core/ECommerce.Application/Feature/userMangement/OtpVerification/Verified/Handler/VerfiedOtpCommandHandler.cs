using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Enum;
using Common.Impl.Result;
using ECommerce.Application.Abstraction.Data;
using Common.Result;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Domain.modules.UserMangement.DomainError;
using ECommerce.Application.Feature.userMangement.OtpVerification.Verified.Command;
using Common.DTOs.UserMangement.OTpVerfrication;
using Common.Exceptions.System.userMangement;

namespace ECommerce.Application.Feature.userMangement.OtpVerification.Verified.Handler
{
    public class VerfiedOtpCommandHandler : IRequestHandler<VerfiedOtpCommand, Result<VerfiedOtpResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService _jwtService;

        public VerfiedOtpCommandHandler(IUnitOfWork unitOfWork, IJWTService _jwtService)
        {
            _unitOfWork = unitOfWork;
            this._jwtService = _jwtService;
        }

        public async Task<Result<VerfiedOtpResponse>> Handle(VerfiedOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(request.email, cancellationToken);

            if (user is null)
            {
                return Result<VerfiedOtpResponse>.Failure(EmailAppError.NotFound);
            }

            var latestOtp = await _unitOfWork.UserOTp.GetLastOtpOfType(user.Id, request.type, cancellationToken);

            if (latestOtp is null || latestOtp.Code.Value != request.otp)
            {
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
                    return Result<VerfiedOtpResponse>.Failure(
                        Error.Validation("OTP.Type", $"Unsupported OTP action: {request.type}"));
            }

            _unitOfWork.Users.Update(user);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var token = _jwtService.GenerateToken(user.Id.ToString(), user.userType.ToString());

                return Result<VerfiedOtpResponse>.Success(
                    new VerfiedOtpResponse(
                        "Verification successful.",
                        user.IsEmailConfirmed,
                        user.AccountStatus,
                        token
                    ));
            }
            catch (Exception)
            {
                return Result<VerfiedOtpResponse>.Failure(
                    Error.Failure("Server.Error", "Failed to finalize verification."));
            }
        }
    }
}
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.userMangement.OtpVerification.Verified.Command;
using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Feature.userMangement.OtpVerification.Resend.Handler
{
    public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, Result<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public ResendOtpCommandHandler(IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ResendOtpCommand command, CancellationToken cancellation = default)
        {
            try
            {
                // 1. Find User
               // var emailSpec = new UserByEmailSpecification(command.email);
                var existUser = await _unitOfWork.Users.GetUserByEmail(command.email, cancellation);

                if (existUser is null)
                {
                    return Result<string>.Failure(EmailAppError.NotFound);
                }

                // 2. Generate new OTP Value Object
                var generatedOtp = OTP.Generate();

 
                var otpEntity = UserOTPEntity.Create(
                    Guid.NewGuid(),
                    existUser.Id,
                    generatedOtp,
                    command.type);

                await _unitOfWork.UserOTp.AddAsync(otpEntity, cancellation);

                await _emailService.SendOtpEmailAsync(existUser.Email.Value, generatedOtp.Value,command.type);

                await _unitOfWork.SaveChangesAsync(cancellation);

                return Result<string>.Success("OTP has been resent successfully.");
            }
            catch (Exception)
            {
                // Returns a generic failure if database or email service fails
                return Result<string>.Failure(Error.Failure("Server.Error", "An error occurred while resending the OTP."));
            }
        }
    }
}
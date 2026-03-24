//using Common.DTOs.IAC.OTpVerfrication;
//using Common.Impl.Result;
//using ECommerce.Application.Abstraction.Data;
//using ECommerce.Application.Feature.IAC.ApplicationError;
//using ECommerce.Application.Feature.IAC.OtpVerification.Verified.Command;
//using ECommerce.Domain.modules.IAC.Specfication;
//using ECommerce.Domain.Modules.IAC.DomainError;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ECommerce.Application.Feature.IAC.OtpVerification.Verified.Handler
//{
//    public class VerfiedOtpCommandHandler : IRequestHandler<VerfiedOtpCommand, Result<VerfiedOtpResponse>>
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public VerfiedOtpCommandHandler(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public Task<Result<VerfiedOtpResponse>> Handle(VerfiedOtpCommand request, CancellationToken cancellationToken)
//        {
//            var EmailSpec = new UserByEmailSpecification(request.email);

//            var ExictingUser = _unitOfWork.Users.GetEntityWithSpec(EmailSpec, cancellationToken);

//            if (ExictingUser is null)
//            {
//                return Task.FromResult(Result<VerfiedOtpResponse>.Failure(EmailAppError.NotFound));
//            }

//            var OtpVerificationResult = _unitOfWork.UserOTp.ge(request.email, request.otp, cancellationToken);



//        }
//    }
//}


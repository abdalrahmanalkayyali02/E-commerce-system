using Common.DTOs.IAC.User;
using Common.Impl.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.IAC.User.UpdateForgetPassword.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.User.UpdateForgetPassword.Handler
{
    public class UpdateForgetPasswordCommandHandler : IRequestHandler<updateForgetPasswordCommand,Result<UpdateForgetPasswordReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IJWTService _jwtService;
        

        public UpdateForgetPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService, IJWTService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public Task<Result<UpdateForgetPasswordReponse>> Handle(updateForgetPasswordCommand command, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}

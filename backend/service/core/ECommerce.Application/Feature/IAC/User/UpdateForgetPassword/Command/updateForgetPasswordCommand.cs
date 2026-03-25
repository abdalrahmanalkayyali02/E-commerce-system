using Common.DTOs.IAC.User;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.User.UpdateForgetPassword.Command
{
    public record updateForgetPasswordCommand(string password) : IRequest<Result<UpdateForgetPasswordReponse>>;
    
    
}

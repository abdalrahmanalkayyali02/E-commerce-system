using Common.DTOs.IAC.User;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Command
{
    public record updateForgetPasswordCommand(string password, string email) : IRequest<Result<UpdateForgetPasswordReponse>>;
    
    
}

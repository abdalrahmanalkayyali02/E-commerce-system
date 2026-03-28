using Common.DTOs.IAC.User;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.User.Login.Command
{
    public record LoginUserCommand(string email, string password) : IRequest<Result<LoginUserResponse>>;
    
}

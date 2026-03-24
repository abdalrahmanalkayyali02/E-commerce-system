using Common.DTOs.IAC.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.User.Login.Command
{
    public record LoginUserCommand(string email, string password) : IRequest<LoginUserResponse>;
    
}

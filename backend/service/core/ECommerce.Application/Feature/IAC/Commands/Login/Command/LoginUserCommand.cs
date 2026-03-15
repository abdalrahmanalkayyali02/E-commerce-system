using ECommerce.Application.DTO.IAC.User.Response;
using IAC.Domain.Value_Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.Commands.Login.Command
{
    public record LoginUserCommand(string email, string password) : IRequest<LoginUserResponse>;
    
}

using IAC.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.IAC.User.Response
{
    public record LoginUserResponse(string Token, bool isVerfiedEmail, AccountStatus status, string message);
    
    
}

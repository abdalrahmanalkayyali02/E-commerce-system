using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.IAC.User
{
    public record UpdateForgetPasswordReponse(string messsage, string token, bool isVerfiedEmail, AccountStatus accountType);
    
    
}

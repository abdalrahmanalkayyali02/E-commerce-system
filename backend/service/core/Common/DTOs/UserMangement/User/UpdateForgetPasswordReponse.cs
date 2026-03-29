using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.UserMangement.User
{
    public record UpdateForgetPasswordReponse(string messsage, string token, bool isVerfiedEmail, AccountStatus accountType);
    
    
}

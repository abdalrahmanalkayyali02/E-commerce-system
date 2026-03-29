using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.UserMangement.User
{
    public record LoginUserResponse(string Token, bool isEmailVerfied, AccountStatus accountStatus);
    
    
    
}

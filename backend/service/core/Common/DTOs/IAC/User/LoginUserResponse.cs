using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.IAC.User
{
    public record LoginUserResponse(string Token, bool isEmailVerfied, AccountStatus accountStatus)
    {
        public string Token { get; }
        public bool IsEmailVerfied { get; }
        public AccountStatus AccountStatus { get; }

       
    }
    
    
}

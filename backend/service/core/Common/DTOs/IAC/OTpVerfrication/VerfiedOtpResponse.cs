using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.IAC.OTpVerfrication
{
    public record VerfiedOtpResponse(string message,bool VerfiedEmail, AccountStatus AccountStatus, string token);
    
    
}

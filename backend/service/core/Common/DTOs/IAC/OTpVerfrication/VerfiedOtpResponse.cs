using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.IAC.OTpVerfrication
{
    public record VerfiedOtpResponse(bool VerfiedEmail, AccountStatus AccountStatus, string token);
    
    
}

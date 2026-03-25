using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions.BussniesLogic
{
    public static class OTpErrorsBL
    {
        public static readonly Error WindowExpired = Error.Validation(
         "Reset.Expired",
         "The reset window has expired. Please verify your OTP again.");

        
    }
}

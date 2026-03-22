using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.ApplicationError
{
    public static class UserNameAppError
    {
        public static readonly Error Unique = Error.Validation
           (
               "DuplicateUserName",
               "User Name is already used "
           );
    }
}


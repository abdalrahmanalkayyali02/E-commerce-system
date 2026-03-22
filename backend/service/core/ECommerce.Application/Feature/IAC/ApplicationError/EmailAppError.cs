using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.ApplicationError
{
    public static class EmailAppError
    {
        public static readonly Error Unique = Error.Validation
         (
          "UserDuplicateEmail",
          "The Email is already used "
         );
    }
}

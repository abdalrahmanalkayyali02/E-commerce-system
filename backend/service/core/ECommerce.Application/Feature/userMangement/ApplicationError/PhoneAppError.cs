using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.ApplicationError
{
    public static class PhoneAppError 
    {
        public static readonly Error Unique = Error.Validation
        (
            "UserDuplicatePhoneNumber",
            "User Phone Number is already used "
        );
    }
}

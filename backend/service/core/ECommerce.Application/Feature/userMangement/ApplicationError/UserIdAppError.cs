using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.ApplicationError
{
    internal class UserIdAppError
    {
        public static readonly Error NotFound = Error.NotFound
            ("UserIdNotFound","The User ID is not found ");

    }
}

using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions.System.userMangement
{
    public class UserIdAppError
    {
        public static readonly Error NotFound = Error.NotFound
            ("UserIdNotFound","The User ID is not found ");

    }
}

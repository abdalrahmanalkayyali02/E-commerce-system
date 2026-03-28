using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.ApplicationError
{
    public static class EmailAppError
    {
        public static readonly Error Unique = Error.Validation
         (
          "UserDuplicateEmail",
          "The Email is already used "
         );


        public static readonly Error NotFound = Error.NotFound
         (
          "UserEmailNotFound",
          "The Email is not found "
         );



    }
}

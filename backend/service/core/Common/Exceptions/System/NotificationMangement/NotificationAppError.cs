using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions.System.NotificationMangement
{
    public static class NotificationAppError
    {
        public static readonly Error NotFound = Error.NotFound(
            "Notification.NotFound",
            "The specified notification was not found.");

         public static readonly Error UnauthorizedAccess = Error.Unauthorized(
            "Notification.UnauthorizedAccess",
            "You do not have permission to access this notification.");
         
        public static readonly Error InvalidOperation = Error.Validation(
            "Notification.InvalidOperation",
            "The operation on the notification is invalid in the current state.");
    }
}

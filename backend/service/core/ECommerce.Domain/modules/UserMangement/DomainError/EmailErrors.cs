using Common.Result;

namespace ECommerce.Domain.modules.UserMangement.DomainError
{
    public static class EmailErrors
    {
        public static readonly Error Required = Error.Validation(
            "Email.Required",
            "Email address is mandatory.");

        public static readonly Error InvalidFormat = Error.Validation(
            "Email.InvalidFormat",
            "The email format is invalid.");

        public static readonly Error TooLong = Error.Validation(
            "Email.TooLong",
            "Email address must be less than 255 characters.");
    }
}
using Common.Result;

namespace ECommerce.Domain.modules.UserMangement.DomainError
{
    public static class PhoneNumberErrors
    {
        public static readonly Error Required = Error.Validation(
            "PhoneNumber.Required",
            "Phone number cannot be empty.");

        public static readonly Error InvalidLength = Error.Validation(
            "PhoneNumber.InvalidLength",
            "Phone number must be between 7 and 15 characters.");

        public static readonly Error InvalidFormat = Error.Validation(
            "PhoneNumber.InvalidFormat",
            "Phone number format is invalid.");
    }
}
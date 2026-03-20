using Common.Result;

namespace ECommerce.Domain.Modules.IAC.DomainError
{
    public static class AddressErrors
    {
        public static readonly Error Required = Error.Validation(
            "Address.Required",
            "User Address is mandatory.");

        public static readonly Error InvalidLength = Error.Validation(
            "Address.InvalidLength",
            "The address must be between 5 and 150 characters.");

        public static readonly Error NotFound = Error.NotFound(
            "Address.NotFound",
            "The specified address was not found.");
    }
}
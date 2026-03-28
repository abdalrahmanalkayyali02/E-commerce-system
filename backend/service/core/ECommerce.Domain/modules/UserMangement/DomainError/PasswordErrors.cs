using Common.Result;

namespace ECommerce.Domain.modules.UserMangement.DomainError
{
    public static class PasswordErrors
    {
        public static readonly Error Required = Error.Validation(
            "Password.Required",
            "Password cannot be empty.");

        public static readonly Error TooShort = Error.Validation(
            "Password.TooShort",
            "Password must be at least 8 characters long.");

        public static readonly Error MissingComplexity = Error.Validation(
            "Password.MissingComplexity",
            "Password must contain at least one uppercase letter, one lowercase letter, and one digit.");
    }
}
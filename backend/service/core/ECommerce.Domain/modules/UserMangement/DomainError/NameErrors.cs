using Common.Result;

namespace ECommerce.Domain.modules.UserMangement.DomainError
{
    public static class NameErrors
    {
        public static readonly Error Required = Error.Validation(
            "Name.Required",
            "Name cannot be empty.");

        public static readonly Error InvalidLength = Error.Validation(
            "Name.InvalidLength",
            "Name must be between 4 and 15 characters long.");

        public static readonly Error ConsecutiveSpaces = Error.Validation(
            "Name.ConsecutiveSpaces",
            "Multiple consecutive spaces are not allowed.");

        public static readonly Error SpecialCharsNotAllowed = Error.Validation(
            "Name.SpecialCharsNotAllowed",
            "Special characters (@ and _) are not allowed in this field.");

        public static readonly Error MultipleSpecialChars = Error.Validation(
            "Name.MultipleSpecialChars",
            "Special characters (@ or _) can only be used once.");

        public static readonly Error InvalidCharacters = Error.Validation(
            "Name.InvalidCharacters",
            "Name contains invalid characters.");
    }
}
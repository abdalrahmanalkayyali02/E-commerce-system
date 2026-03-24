using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class AltTextError
    {
        public static readonly Error Required = Error.Validation(
            "AltText.Required", "Alternative text is mandatory for SEO.");

        public static readonly Error InvalidFormat = Error.Validation(
            "AltText.InvalidFormat", "Alt text can only contain letters, numbers, and single spaces.");

        public static readonly Error TooLong = Error.Validation(
            "AltText.TooLong", "Alt text cannot exceed 150 characters.");
    }
}
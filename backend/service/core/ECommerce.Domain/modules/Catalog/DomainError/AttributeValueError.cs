using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class AttributeValueError
    {
        public static readonly Error Required = Error.Validation(
            "AttributeValue.Required",
            "Product Attribute Value is mandatory.");

        public static readonly Error InvalidLength = Error.Validation(
            "AttributeValue.InvalidLength",
            "Attribute Value cannot exceed 100 characters.");

        public static readonly Error InvalidFormat = Error.Validation(
            "AttributeValue.InvalidFormat",
            "Invalid format: No special characters allowed, and only a single space between words.");
    }
}
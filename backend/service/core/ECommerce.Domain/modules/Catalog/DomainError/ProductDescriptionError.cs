using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class ProductDescriptionError
    {
        public static readonly Error Required = Error.Validation(
            "ProductDescription.Required",
            "The product description is mandatory.");

        public static readonly Error TooLong = Error.Validation(
            "ProductDescription.TooLong",
            "The product description cannot exceed 2000 characters.");

        public static readonly Error InvalidFormat = Error.Validation(
            "ProductDescription.InvalidFormat",
            "Description can only contain letters, numbers, and single spaces.");
    }
}
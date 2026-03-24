using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class ProductNameError
    {
        public static readonly Error Required = Error.Validation(
            "ProductName.Required", "Product name is mandatory.");

        public static readonly Error InvalidLength = Error.Validation(
            "ProductName.InvalidLength", "Product name must be between 3 and 100 characters.");

        public static readonly Error InvalidFormat = Error.Validation(
            "ProductName.InvalidFormat", "Product name can only contain letters, numbers, and single spaces.");
    }
}
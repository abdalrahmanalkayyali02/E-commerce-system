using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class CategoryNameError
    {
        public static readonly Error Required = Error.Validation(
            "CategoryName.Required",
            "The category name is mandatory.");

        public static readonly Error InvalidLength = Error.Validation(
            "CategoryName.InvalidLength",
            "The category name must be between 3 and 50 characters.");

        public static readonly Error InvalidFormat = Error.Validation(
            "CategoryName.InvalidFormat",
            "Category name can only contain letters, numbers, the '&' symbol, and single spaces.");
    }
}
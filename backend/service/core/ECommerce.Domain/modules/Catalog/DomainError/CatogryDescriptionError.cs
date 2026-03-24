using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class CategoryDescriptionError
    {
        public static readonly Error Required = Error.Validation(
            "CategoryDescription.Required",
            "The category description is mandatory.");

        public static readonly Error InvalidLength = Error.Validation(
            "CategoryDescription.InvalidLength",
            "The description is too long. Maximum allowed is 500 characters.");

        public static readonly Error InvalidFormat = Error.Validation(
            "CategoryDescription.InvalidFormat",
            "The description contains invalid characters.");
    }
}
using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class PhotoUrlError
    {
        public static readonly Error Required = Error.Validation(
            "PhotoUrl.Required", "The photo URL is mandatory.");

        public static readonly Error InvalidFormat = Error.Validation(
            "PhotoUrl.InvalidFormat", "The provided URL is not a valid absolute HTTP/HTTPS link.");

        public static readonly Error TooLong = Error.Validation(
            "PhotoUrl.TooLong", "The URL exceeds the maximum allowed length (2048 characters).");
    }
}
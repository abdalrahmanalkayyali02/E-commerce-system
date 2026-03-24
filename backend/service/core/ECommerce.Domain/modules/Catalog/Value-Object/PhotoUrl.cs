using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record PhotoUrl
    {
        public string Value { get; init; }

        private PhotoUrl(string value) => Value = value;

        public static Result<PhotoUrl> Create(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return PhotoUrlError.Required;

            var trimmedUrl = url.Trim();

            // 1. Validate it is a valid Absolute URL
            if (!Uri.TryCreate(trimmedUrl, UriKind.Absolute, out var uriResult))
            {
                return PhotoUrlError.InvalidFormat;
            }

            return Result<PhotoUrl>.Success(new PhotoUrl(trimmedUrl));
        }

        public static PhotoUrl Reconstruct(string value) => new PhotoUrl(value);
        public override string ToString() => Value;
    }
}
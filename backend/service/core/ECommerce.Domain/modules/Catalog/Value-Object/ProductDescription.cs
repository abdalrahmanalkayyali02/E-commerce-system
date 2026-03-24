using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record ProductDescription
    {
        private static readonly Regex ValidRegex = new(
            @"^[a-zA-Z0-9]+( [a-zA-Z0-9]+)*$",
            RegexOptions.Compiled);

        public string Value { get; init; }

        private ProductDescription(string value) => Value = value;

        public static Result<ProductDescription> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ProductDescriptionError.Required;

            var processed = value.Trim();

            if (processed.Length > 2000)
                return ProductDescriptionError.TooLong;

            if (!ValidRegex.IsMatch(processed))
                return ProductDescriptionError.InvalidFormat;

            return Result<ProductDescription>.Success(new ProductDescription(processed));
        }


        public static ProductDescription Reconstruct(string value) => new ProductDescription(value);

        public override string ToString() => Value;
    }
}
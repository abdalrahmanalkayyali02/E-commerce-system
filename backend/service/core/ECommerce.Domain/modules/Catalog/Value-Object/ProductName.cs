using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record ProductName
    {

        private static readonly Regex NameRegex = new(
            @"^[a-zA-Z0-9&]+( [a-zA-Z0-9&]+)*$",
            RegexOptions.Compiled);

        public string Value { get; init; }

        private ProductName(string value) => Value = value;

        public static Result<ProductName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ProductNameError.Required;

            var processed = value.Trim();

            if (processed.Length < 3 || processed.Length > 100)
                return ProductNameError.InvalidLength;

            if (!NameRegex.IsMatch(processed))
                return ProductNameError.InvalidFormat;

            return Result<ProductName>.Success(new ProductName(processed));
        }
        public static ProductName Reconstruct(string value) => new ProductName(value);

        public override string ToString() => Value;
    }
}
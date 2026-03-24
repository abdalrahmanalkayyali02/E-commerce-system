using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record CategoryName
    {

        private static readonly Regex NameRegex = new(
            @"^[a-zA-Z0-9&]+( [a-zA-Z0-9&]+)*$",
            RegexOptions.Compiled);

        public string Value { get; init; }

        private CategoryName(string value) => Value = value;

        public static Result<CategoryName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return CategoryNameError.Required;

            var processed = value.Trim();

            if (processed.Length < 3 || processed.Length > 50)
                return CategoryNameError.InvalidLength;

            // Pattern matching
            if (!NameRegex.IsMatch(processed))
                return CategoryNameError.InvalidFormat;

            return Result<CategoryName>.Success(new CategoryName(processed));
        }

        public static CategoryName Reconstruct(string value) => new CategoryName(value);

        public override string ToString() => Value;
    }
}
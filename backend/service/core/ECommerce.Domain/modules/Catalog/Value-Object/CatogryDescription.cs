using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record CategoryDescription
    {
        private static readonly Regex DescriptionRegex = new(
            @"^[a-zA-Z0-9]+( [a-zA-Z0-9]+)*$",
            RegexOptions.Compiled);

        public string Value { get; init; }

        private CategoryDescription(string value) => Value = value;

        public static Result<CategoryDescription> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return CategoryDescriptionError.Required;

            var processed = value.Trim();

            if (processed.Length > 500)
                return CategoryDescriptionError.InvalidLength;

            if (!DescriptionRegex.IsMatch(processed))
                return CategoryDescriptionError.InvalidFormat;

            return Result<CategoryDescription>.Success(new CategoryDescription(processed));
        }

        public static CategoryDescription Reconstruct(string value) => new CategoryDescription(value);

        public override string ToString() => Value;
    }
}
using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record AltText
    {

        private static readonly Regex ValidRegex = new(
            @"^[a-zA-Z0-9]+( [a-zA-Z0-9]+)*$",
            RegexOptions.Compiled);

        public string Value { get; init; }

        private AltText(string value) => Value = value;

        public static Result<AltText> Create(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return AltTextError.Required;

            var processed = text.Trim();

            if (processed.Length > 150)
                return AltTextError.TooLong;

            if (!ValidRegex.IsMatch(processed))
                return AltTextError.InvalidFormat;

            return Result<AltText>.Success(new AltText(processed));
        }


        public static AltText Reconstruct(string value) => new AltText(value);

        public override string ToString() => Value;
    }
}
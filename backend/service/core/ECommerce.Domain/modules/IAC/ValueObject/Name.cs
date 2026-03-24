using Common.Impl.Result;
using ECommerce.Domain.Modules.IAC.DomainError;
using System.Linq;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.IAC.ValueObject
{
    public sealed record Name
    {
        public string Value { get; init; }

        private Name(string value)
        {
            Value = value;
        }

        public static Result<Name> From(string value) => Create(value, allowSpecialChars: true);

        public static Result<Name> FromStrict(string value) => Create(value, allowSpecialChars: false);

        private static Result<Name> Create(string value, bool allowSpecialChars)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return NameErrors.Required;
            }

            var trimmedName = value.Trim();

            if (trimmedName.Length < 3 || trimmedName.Length > 15)
            {
                return NameErrors.InvalidLength;
            }

            if (trimmedName.Contains("  "))
            {
                return NameErrors.ConsecutiveSpaces;
            }

            if (!allowSpecialChars)
            {
                if (trimmedName.Contains("@") || trimmedName.Contains("_"))
                {
                    return NameErrors.SpecialCharsNotAllowed;
                }
            }
            else
            {
                if (trimmedName.Count(c => c == '_') > 1 || trimmedName.Count(c => c == '@') > 1)
                {
                    return NameErrors.MultipleSpecialChars;
                }
            }

            if (!Regex.IsMatch(trimmedName, @"^[a-zA-Z0-9 _@]+$"))
            {
                return NameErrors.InvalidCharacters;
            }

            return new Name(trimmedName);
        }

        public static Name Reconstruct(string value) => new(value);

        public override string ToString() => Value;
    }
}
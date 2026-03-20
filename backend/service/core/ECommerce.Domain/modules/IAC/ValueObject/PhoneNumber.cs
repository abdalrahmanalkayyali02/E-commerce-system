using Common.Impl.Result;
using ECommerce.Domain.Modules.IAC.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.Modules.IAC.ValueObject
{
    public sealed record PhoneNumber
    {
        public string Value { get; init; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return PhoneNumberErrors.Required;
            }

            var cleanedValue = value.Trim().Replace(" ", "").Replace("-", "");

            if (cleanedValue.Length < 7 || cleanedValue.Length > 15)
            {
                return PhoneNumberErrors.InvalidLength;
            }

            if (!Regex.IsMatch(cleanedValue, @"^\+?[0-9]+$"))
            {
                return PhoneNumberErrors.InvalidFormat;
            }

            return new PhoneNumber(cleanedValue);
        }

 
        public static PhoneNumber Reconstruct(string value) => new(value);

        public override string ToString() => Value;
    }
}
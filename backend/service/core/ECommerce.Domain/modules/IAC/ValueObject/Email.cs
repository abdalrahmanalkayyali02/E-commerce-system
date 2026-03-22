using Common.Impl.Result;
using ECommerce.Domain.Modules.IAC.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.IAC.ValueObject
{
    public sealed record Email
    {
        public string Value { get; init; }

        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<Email>.Failure(EmailErrors.Required);
            }

            var trimmedEmail = value.Trim();

            if (!EmailRegex.IsMatch(trimmedEmail))
            {
                return Result<Email>.Failure(EmailErrors.InvalidFormat);
            }

            return Result <Email>.Success(new Email(trimmedEmail.ToLowerInvariant()));
        }

        public static Email Reconstruct(string value) => new(value);

        public override string ToString() => Value;
    }
}
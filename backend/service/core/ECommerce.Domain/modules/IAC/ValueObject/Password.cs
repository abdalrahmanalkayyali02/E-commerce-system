using System;
using System.Linq;
using Common.Impl.Result;
using ECommerce.Domain.Modules.IAC.DomainError;

namespace ECommerce.Domain.Modules.IAC.ValueObject
{
    public sealed record Password
    {
        public string Value { get; init; }

        private Password(string value)
        {
            Value = value;
        }

        public static Result<Password> From(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return PasswordErrors.Required;
            }

            if (password.Length < 8)
            {
                return PasswordErrors.TooShort;
            }

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);

            if (!hasUpper || !hasLower || !hasDigit)
            {
                return PasswordErrors.MissingComplexity;
            }


            return Result<Password>.Success(new Password(password));
        }

        public static Password Reconstruct(string value) => new(value);

        public override string ToString() => "********";
    }
}
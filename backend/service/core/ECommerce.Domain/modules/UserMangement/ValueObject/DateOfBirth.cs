using Common.Impl.Result;
using ECommerce.Domain.modules.UserMangement.DomainError;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.UserMangement.ValueObject
{
    public sealed record DateOfBirth
    {
        public DateOnly Value { get; init; }

        private DateOfBirth(DateOnly value) => Value = value;

        /// <summary>
        /// Used when creating a new request of the DOB from the user.
        /// Expected format: yyyy-MM-dd (e.g. 2000-01-25)
        /// </summary>
        public static Result<DateOfBirth> From(string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr) || !Regex.IsMatch(dateStr, @"^\d{4}-\d{2}-\d{2}$"))
                return Result<DateOfBirth>.Failure(DateOfBirthErrors.InvalidFormat);

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return Result<DateOfBirth>.Failure(DateOfBirthErrors.InvalidFormat);

            var utcDateTime = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            var date = DateOnly.FromDateTime(utcDateTime);
            var today = DateOnly.FromDateTime(DateTime.Today);

            // Future date check
            if (date > today)
                return Result<DateOfBirth>.Failure(DateOfBirthErrors.FutureDate);

            // Accurate age calculation
            int age = today.Year - date.Year;
            if (date > today.AddYears(-age))
                age--;

            if (age < 18 || age > 150)
                return Result<DateOfBirth>.Failure(DateOfBirthErrors.InvalidAge);

            return Result<DateOfBirth>.Success(new DateOfBirth(date));
        }

        /// <summary>
        /// Used when reconstructing a DateOfBirth from a persisted/trusted source (e.g. database).
        /// Skips all validation.
        /// </summary>
        public static DateOfBirth Reconstructing(DateOnly date)
        {
            return new DateOfBirth(date);
        }

        public static implicit operator DateOnly(DateOfBirth dob) => dob.Value;

        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}
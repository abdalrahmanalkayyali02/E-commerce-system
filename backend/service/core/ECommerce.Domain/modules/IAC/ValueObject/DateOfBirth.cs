using Common.Impl.Result;
using ECommerce.Domain.Modules.IAC.DomainError;
using System.Globalization;

public sealed record DateOfBirth
{
    public DateOnly Value { get; init; }

    private DateOfBirth(DateOnly value) => Value = value;

    /// <summary>
    /// used when creating new request of the dob form user
    /// </summary>
    public static Result<DateOfBirth> From(string dateStr)
    {
        if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
        {
            return Result<DateOfBirth>.Failure(DateOfBirthErrors.InvalidFormat);
        }

        var utcDateTime = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        var date = DateOnly.FromDateTime(utcDateTime);

        int age = DateTime.Today.Year - date.Year;

        if (age < 18 || age > 150)
        {
            return Result<DateOfBirth>.Failure(DateOfBirthErrors.InvalidAge);

        }

        return Result<DateOfBirth>.Success(new DateOfBirth(date));
    }


    public static DateOfBirth Reconstructing(DateOnly date)
    {
        return new DateOfBirth(date);
    }

    public static implicit operator DateOnly(DateOfBirth dob) => dob.Value;

    public override string ToString() => Value.ToString("yyyy-MM-dd");
}
using System.Globalization;

namespace IAC.Domain.Value_Object;

public record DateOfBirth
{
    public DateTime Value { get; }

    private DateOfBirth(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Date of birth cannot be empty.");

        if (!DateTime.TryParseExact(value, "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime parsedDate))
        {
            throw new ArgumentException("Invalid date format. Please use DD-MM-YYYY (e.g., 25-12-1990).");
        }

        var today = DateTime.UtcNow.Date;
        var age = today.Year - parsedDate.Year;

        if (parsedDate.Date > today.AddYears(-age)) age--;

        if (age < 18)
            throw new ArgumentException("User must be at least 18 years old.");

        if (age > 150)
            throw new ArgumentException("User must be at most 150 years old");

        Value = parsedDate;
    }

    public static DateOfBirth From(string value) => new(value);

    public override string ToString() => Value.ToString("dd-MM-yyyy");
}
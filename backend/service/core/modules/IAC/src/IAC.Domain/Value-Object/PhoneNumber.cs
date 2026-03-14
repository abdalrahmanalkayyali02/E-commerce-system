namespace IAC.Domain.Value_Object;

public record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Phone number cannot be empty.");
        }

        var cleanedValue = value.Trim().Replace(" ", "").Replace("-", "");

        if (cleanedValue.Length < 7 || cleanedValue.Length > 15)
        {
            throw new ArgumentException("Phone number must be between 7 and 15 characters.");
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(cleanedValue, @"^\+?[0-9]+$"))
        {
            throw new ArgumentException("Phone number format is invalid.");
        }

        Value = cleanedValue;
    }

    public static PhoneNumber From(string value) => new(value);

    public override string ToString() => Value;
}
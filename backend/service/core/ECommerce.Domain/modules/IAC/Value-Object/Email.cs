using System.Text.RegularExpressions;

namespace IAC.Domain.Value_Object;

public record Email
{
    public string Value { get; }

    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be empty.");
        }

        var trimmedEmail = value.Trim();

        if (!EmailRegex.IsMatch(trimmedEmail))
        {
            throw new ArgumentException("Invalid email format.");
        }

        Value = trimmedEmail.ToLowerInvariant();
    }

    public static Email From(string value) => new(value: value);

    public override string ToString() => Value;
}
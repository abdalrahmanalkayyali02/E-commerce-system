namespace IAC.Domain.Value_Object;

public class Name
{
    public string Value { get; }

    private Name(string value, bool allowSpecialChars)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty.");

        var trimmedName = value.Trim();

        if (trimmedName.Length < 4 || trimmedName.Length > 15)
            throw new ArgumentException("Name must be between 4 and 15 characters long.");

        if (trimmedName.Contains("  "))
            throw new ArgumentException("Multiple consecutive spaces are not allowed.");

        if (!allowSpecialChars)
        {
            if (trimmedName.Contains("@") || trimmedName.Contains("_"))
                throw new ArgumentException("Special characters (@ and _) are not allowed in this field.");
        }
        else
        {
            if (trimmedName.Count(c => c == '_') > 1)
                throw new ArgumentException("The underscore (_) can only be used once.");

            if (trimmedName.Count(c => c == '@') > 1)
                throw new ArgumentException("The @ symbol can only be used once.");
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(trimmedName, @"^[a-zA-Z0-9 _@]+$"))
            throw new ArgumentException("Name contains invalid characters.");

        Value = trimmedName;
    }

    public static Name From(string value) => new(value, allowSpecialChars: true);

    public static Name FromStrict(string value) => new(value, allowSpecialChars: false);

    public override string ToString() => Value;
}
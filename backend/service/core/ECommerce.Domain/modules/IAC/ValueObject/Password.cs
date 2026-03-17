namespace ECommerce.Domain.modules.IAC.ValueObject;

public sealed record Password
{
    public string Value { get; }

    private Password(string value)
    {
        Validate(value);
        Value = value;
    }

    public static Password From(string value) => new(value);

    private static void Validate(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("the password can not be empty !!");

        if (password.Length < 8)
            throw new ArgumentException("the password must be at least 8 char ");

        if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit))
            throw new ArgumentException("the password must have one uppercase and one lowercase ");
    }

    public override string ToString() => "********";
}
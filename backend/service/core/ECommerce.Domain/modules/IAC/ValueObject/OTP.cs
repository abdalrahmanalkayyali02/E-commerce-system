namespace ECommerce.Domain.modules.IAC.ValueObject;

public sealed record OTP
{
    public string Value { get; }

    private OTP(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 6 || !value.All(char.IsLetterOrDigit))
        {
            throw new ArgumentException("OTP must be 6 alphanumeric characters.");
        }

        Value = value.ToUpper();
    }

    public static OTP From(string value) => new(value);

    public static OTP Generate()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var random = new Random();
        var code = new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return new OTP(code);
    } 


    public static OTP Reconstructing(string value)
    {
        return new OTP(value);
    }
}
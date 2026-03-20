namespace Common.DTOs.IAC.Request
{
    public abstract record CreateUserBaseRequest(
        string FirstName,
        string LastName,
        string UserName,
        string DateOfBirth, // Consider DateOnly for better type safety
        string Email,
        string PhoneNumber,
        string Password,
        string? ProfilePhoto
    );

    // 2. Specific Customer Record (Adds the Address)
    public record CreateCustomerRequest(
        string FirstName, string LastName, string UserName,
        string DateOfBirth, string Email, string PhoneNumber,
        string Password, string? ProfilePhoto,
        string Address
    ) : CreateUserBaseRequest(FirstName, LastName, UserName, DateOfBirth, Email, PhoneNumber, Password, ProfilePhoto);

    // 3. Example: An Admin Record (No Address needed)
    public record CreateAdminRequest(
        string FirstName, string LastName, string UserName,
        string DateOfBirth, string Email, string PhoneNumber,
        string Password, string? ProfilePhoto,
        string Department
    ) : CreateUserBaseRequest(FirstName, LastName, UserName, DateOfBirth, Email, PhoneNumber, Password, ProfilePhoto);
}
namespace Common.DTOs.IAC.User
{
    public abstract record CreateUserBaseRequest(
        string FirstName,
        string LastName,
        string UserName,
        string DateOfBirth, 
        string Email,
        string PhoneNumber,
        string Password,
        string? ProfilePhoto
    );

    public record CreateCustomerRequest(
        string FirstName, string LastName, string UserName,
        string DateOfBirth, string Email, string PhoneNumber,
        string Password, string? ProfilePhoto,
        string Address
    ) : CreateUserBaseRequest(FirstName, LastName, UserName, DateOfBirth, Email, PhoneNumber, Password, ProfilePhoto);

    public record CreateAdminRequest(
        string FirstName, string LastName, string UserName,
        string DateOfBirth, string Email, string PhoneNumber,
        string Password, string? ProfilePhoto,
        string Department
    ) : CreateUserBaseRequest(FirstName, LastName, UserName, DateOfBirth, Email, PhoneNumber, Password, ProfilePhoto);
}
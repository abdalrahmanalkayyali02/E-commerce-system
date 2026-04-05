namespace Api.ViewModels.web.UserMangement.Request
{
    public record RegisterCustomerRequest(
        string FirstName,
        string LastName,
        string UserName,
        string DateOfBirth,
        string Email,
        string PhoneNumber,
        string Password,
        string Address,
        IFormFile? ProfilePhoto 
    );
}

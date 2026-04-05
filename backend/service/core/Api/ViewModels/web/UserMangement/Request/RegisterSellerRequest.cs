namespace Api.ViewModels.web.UserMangement.Request
{
    public record RegisterSellerRequest
    (
        string FirstName,
        string LastName,
        string UserName,
        string DateOfBirth,
        string Email,
        string PhoneNumber,
        string Password,
        string Address,
        IFormFile? ProfilePhoto,
        string shopName,
        IFormFile shopPhoto,
        IFormFile verfiedSellerDocument,
        IFormFile verfiedShopDocument
    );
    
}

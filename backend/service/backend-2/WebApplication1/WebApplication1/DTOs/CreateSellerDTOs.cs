using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.DTOs
{
    public record CreateSellerDtOs
    (
        string FirstName,
        string LastName,
        string UserName,
        DateOnly DateOfBirth,
        string Email,
        string PhoneNumber,
        string Password,
        IFormFile? ProfilePhoto,
        string Address,
        string ShopName,
        IFormFile ShopPhoto,
        IFormFile VerfiedSellerDocument,
        IFormFile VerfiedShopDocument
    );
}

using MediatR;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.DTOs
{
    public record CreateSellerDtOs
    (
        string FirstName,
        string LastName,
        string UserName,
        string DateOfBirth,
        string Email,
        string PhoneNumber,
        string Password,
        Stream? ProfilePhoto,
        string Address,
        string ShopName,
        Stream ShopPhoto,
        Stream VerfiedSellerDocument,
        Stream VerfiedShopDocument
    ):IRequest<Result<CreateUserResponse>>;
}

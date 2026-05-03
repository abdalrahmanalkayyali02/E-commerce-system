using MediatR;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.DTOs
{
    public record CreateSellerDTOs
    (
        string firstName,
        string lastName,
        string userName,
        string dateOfBirth,
        string email,
        string phoneNumber,
        string password,
        Stream? profilePhoto,
        string address,
        string shopName,
        Stream shopPhoto,
        Stream verfiedSellerDocument,
        Stream verfiedShopDocument
    ):IRequest<Result<CreateUserResponse>>;
}

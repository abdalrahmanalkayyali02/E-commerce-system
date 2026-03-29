using Common.Enum;

namespace Common.DTOs.UserMangement.User
{

    public record UserResponse(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        DateOnly DateOfBirth,
        string? ProfilePhoto,
        UserType UserType,
        AccountStatus AccountStatus,
        DateTime CreateAt
    );

    public record CustomerResponse(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        DateOnly DateOfBirth,
        string? ProfilePhoto,
        UserType UserType,
        AccountStatus AccountStatus,
        DateTime CreateAt,
        string Address
    );

    public record SellerResponse(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        DateOnly DateOfBirth,
        string? ProfilePhoto,
        UserType UserType,
        AccountStatus AccountStatus,
        DateTime CreateAt,
        string ShopName,
        string ShopPhoto,
        string Address,
        bool IsVerifiedByAdmin,
        string VerifiedSellerDocument,
        string VerifiedShopDocument
    );
}
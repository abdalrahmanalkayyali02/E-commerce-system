using Common.Enum;

namespace Common.DTOs.IAC.User
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
        UserResponse UserInfo, 
        string Address
    ) : UserResponse(UserInfo.FirstName, UserInfo.LastName, UserInfo.UserName, UserInfo.Email,
                     UserInfo.PhoneNumber, UserInfo.DateOfBirth, UserInfo.ProfilePhoto,
                     UserInfo.UserType, UserInfo.AccountStatus, UserInfo.CreateAt);

    public record SellerResponse(
        UserResponse UserInfo,
        string ShopName,
        string ShopPhoto,
        string Address,
        bool IsVerifiedByAdmin,
        string VerifiedSellerDocument,
        string VerifiedShopDocument
    ) : UserResponse(UserInfo.FirstName, UserInfo.LastName, UserInfo.UserName, UserInfo.Email,
                     UserInfo.PhoneNumber, UserInfo.DateOfBirth, UserInfo.ProfilePhoto,
                     UserInfo.UserType, UserInfo.AccountStatus, UserInfo.CreateAt);
}
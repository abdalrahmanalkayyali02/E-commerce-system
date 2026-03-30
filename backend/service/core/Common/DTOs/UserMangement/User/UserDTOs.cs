using Common.Enum;
using System;

namespace Common.DTOs.UserMangement.User
{

    public record UserDto
    (
        Guid UserId,
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        AccountStatus AccountStatus,
        bool IsDelete
    );
}
using WebApplication1.Shared.Enum;

namespace WebApplication1.DTOs;

public record UserDtOs(
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
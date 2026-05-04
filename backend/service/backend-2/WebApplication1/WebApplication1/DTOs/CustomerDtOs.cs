using WebApplication1.Shared.Enum;

namespace WebApplication1.DTOs;

public record CustomerDtOs
(
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
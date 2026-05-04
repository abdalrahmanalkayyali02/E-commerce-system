namespace WebApplication1.DTOs;


public record UpdateSellerProfileDtOs
(
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    IFormFile? ProfilePhoto,
    string? Address
);
namespace WebApplication1.DTOs;

public record UpdateUserProfileDtOs
(            
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    IFormFile? ProfilePhoto
    );
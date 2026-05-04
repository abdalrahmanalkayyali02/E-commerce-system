using WebApplication1.Shared.Enum;

namespace WebApplication1.DTOs;


public record UpdateForgetPasswordDtOsResponse
    (string Messsage, string Token, bool IsVerfiedEmail, AccountStatus AccountType);
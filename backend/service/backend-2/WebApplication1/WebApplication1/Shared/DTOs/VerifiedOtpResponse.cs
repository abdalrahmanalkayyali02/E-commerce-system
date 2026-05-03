using WebApplication1.Shared.Enum;

namespace WebApplication1.Shared.DTOs
{
   public record VerifiedOtpResponse(string message, bool VerifiedEmail, AccountStatus AccountStatus, string token);
}

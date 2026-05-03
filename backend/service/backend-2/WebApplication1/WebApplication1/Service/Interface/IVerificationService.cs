using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface
{
    public interface IVerificationService
    {
        public Result<string> ResendOtpService(string email);
        public Result<VerifiedOtpResponse> VerifiedOtpService(string email, string otp, OtpType type);
    }
}
    
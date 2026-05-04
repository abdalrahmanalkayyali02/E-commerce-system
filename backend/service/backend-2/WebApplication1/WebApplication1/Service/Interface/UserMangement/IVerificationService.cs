using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface.UserMangement
{
    public interface IVerificationService
    {
        public Task<Result<string>> ResendOtpService(string email,OtpType type,CancellationToken ct);
        public Task<Result<VerifiedOtpResponse>> VerifiedOtpService(string email, string otp, OtpType type,CancellationToken ct);
    }
}
    
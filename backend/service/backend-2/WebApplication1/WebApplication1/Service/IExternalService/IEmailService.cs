using WebApplication1.Shared.Enum;

namespace WebApplication1.Service.IExternalService;

    public interface IEmailService
    {
        Task SendOtpEmailAsync(string userEmail, string otp, OtpType type);
    }
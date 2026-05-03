using WebApplication1.Shared.Enum;

namespace WebApplication1.Service.IExternalService.Abstraction;

    public interface IEmailService
    {
        Task SendOtpEmailAsync(string userEmail, string otp, OtpType type);
    }
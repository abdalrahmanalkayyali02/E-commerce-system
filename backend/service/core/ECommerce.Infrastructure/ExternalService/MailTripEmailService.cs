using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Common.Enum;
using ECommerce.Application.Abstraction.IExternalService;

namespace ECommerce.Infrastructure.ExternalService
{
    public class MailTripEmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public MailTripEmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpEmailAsync(string userEmail, string otp, EmailOtpType type)
        {
            var host = _config["MailSettings:Host"] ?? "live.smtp.mailtrap.io";
            var port = int.Parse(_config["MailSettings:Port"] ?? "587");
            var apiToken = _config["MailSettings:ApiToken"];
            var fromEmail = _config["MailSettings:FromEmail"] ?? "no-reply@ecommerce-store.com";
            var fromName = _config["MailSettings:FromName"] ?? "E-Commerce Store";

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential("api", apiToken),
                EnableSsl = true
            };

            var (subject, body) = GetTemplate(type, otp);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(userEmail);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"E-commerce email delivery failed for {userEmail}.", ex);
            }
        }

        private (string Subject, string Body) GetTemplate(EmailOtpType type, string otp)
        {
            // E-commerce Styling: Dark Slate and Gold/Amber accents
            const string primaryColor = "#0f172a"; // Deep Slate
            const string accentColor = "#f59e0b";  // Amber/Gold
            const string bgColor = "#f8fafc";

            string GetHtmlWrapper(string title, string content) => $@"
                <div style=""background-color: {bgColor}; padding: 40px 10px; font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif; color: #334155;"">
                    <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px; background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 4px;"">
                        <tr>
                            <td style=""padding: 30px; text-align: left; border-bottom: 4px solid {accentColor};"">
                                <span style=""font-size: 24px; font-weight: 800; color: {primaryColor}; text-transform: uppercase; letter-spacing: 1px;"">
                                    E-COMMERCE <span style=""color: {accentColor};"">STORE</span>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style=""padding: 40px 30px;"">
                                <h2 style=""margin-top: 0; color: {primaryColor}; font-size: 20px; font-weight: 700;"">{title}</h2>
                                <div style=""font-size: 15px; line-height: 1.6; color: #475569;"">
                                    {content}
                                </div>
                                <div style=""margin: 35px 0; padding: 30px; background-color: {primaryColor}; border-radius: 4px; text-align: center;"">
                                    <p style=""margin: 0 0 10px 0; font-size: 12px; color: #94a3b8; text-transform: uppercase; letter-spacing: 2px;"">Secure Access Code</p>
                                    <span style=""font-family: 'Courier New', monospace; font-size: 38px; font-weight: 700; color: {accentColor}; letter-spacing: 10px;"">{otp}</span>
                                </div>
                                <p style=""font-size: 13px; color: #64748b;"">
                                    For your security, please do not share this code with anyone. Our support team will never ask for your OTP.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style=""padding: 25px 30px; background-color: #f1f5f9; text-align: center;"">
                                <p style=""margin: 0; font-size: 12px; color: #94a3b8;"">
                                    &copy; {DateTime.Now.Year} E-Commerce Store. Amman, Jordan.<br>
                                    Providing quality products since 2024.
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>";

            return type switch
            {
                EmailOtpType.Registration => (
                    "Welcome! Verify your E-Commerce Store account",
                    GetHtmlWrapper("Finish Setting Up Your Account",
                        "<p>Welcome to our community! You're just one step away from exclusive deals and a faster checkout experience. Please verify your email using the code below.</p>")
                ),

                EmailOtpType.ForgotPassword => (
                    "Password Reset: Verification Code",
                    GetHtmlWrapper("Security Verification",
                        "<p>We received a request to reset your store password. Use the following code to authorize this change. If this wasn't you, please secure your account immediately.</p>")
                ),

                _ => (
                    "Store Security Code",
                    GetHtmlWrapper("Verify Your Transaction",
                        "<p>Please enter the following code to confirm your identity and proceed with your current request.</p>")
                )
            };
        }
    }
}
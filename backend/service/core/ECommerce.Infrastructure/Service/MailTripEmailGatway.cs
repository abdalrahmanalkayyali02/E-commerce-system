using IAC.Application.Abstraction;
using IAC.Domain.Enum;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure.Service
{
    public class MailTripEmailGatway : IEmailGatway
    {
        private readonly IConfiguration _config;

        public MailTripEmailGatway(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpEmailAsync(string userEmail, string otp, EmailOtpType type)
        {
            // Fetch credentials from config
            var host = _config["MailSettings:Host"];
            var port = int.Parse(_config["MailSettings:Port"] ?? "2525");
            var user = _config["MailSettings:User"];
            var pass = _config["MailSettings:Pass"];

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_config["MailSettings:FromName"], _config["MailSettings:FromEmail"]));
            email.To.Add(MailboxAddress.Parse(userEmail));

            // Dynamic Content based on Enum
            var (subject, body) = GetTemplate(type, otp);
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                // Mailtrap Sandbox uses StartTls on 2525 or 587
                await smtp.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(user, pass);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Rethrow or log for your E-Commerce error handling middleware
                throw new Exception("Email delivery failed.", ex);
            }
        }

        private (string Subject, string Body) GetTemplate(EmailOtpType type, string otp)
        {
            return type switch
            {
                EmailOtpType.Registration => ("Verify Your Store Account",
                    $"<h1>Welcome!</h1><p>Use code <b>{otp}</b> to verify your e-commerce account.</p>"),

                EmailOtpType.ForgotPassword => ("Password Reset Request",
                    $"<h1>Reset Password</h1><p>Your security code is: <b>{otp}</b></p>"),

                _ => ("Security Verification", $"<p>Your OTP code is: {otp}</p>")
            };
        }
    }
}
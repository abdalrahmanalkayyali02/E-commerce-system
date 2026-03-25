using Common.Result;

namespace ECommerce.Domain.Modules.IAC.DomainError
{
    public static class OtpDomainErrors
    {
        public static readonly Error Expired = Error.Validation(
            "OTP.Expired",
            "The OTP code has expired. Please request a new one.");

        public static readonly Error AlreadyUsed = Error.Conflict(
            "OTP.AlreadyUsed",
            "This OTP has already been used.");

        public static readonly Error TooManyAttempts = Error.Validation(
            "OTP.TooManyAttempts",
            "Too many failed attempts. Security lock engaged.");
    }
}
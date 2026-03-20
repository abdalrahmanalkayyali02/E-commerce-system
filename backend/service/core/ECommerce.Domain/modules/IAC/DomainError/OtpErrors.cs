using Common.Result;

namespace ECommerce.Domain.Modules.IAC.DomainError
{
    public static class OtpErrors
    {
        public static readonly Error Required = Error.Validation(
            "OTP.Required",
            "OTP code is mandatory.");

        public static readonly Error InvalidFormat = Error.Validation(
            "OTP.InvalidFormat",
            "The OTP must be a 6-digit numeric code.");

        public static readonly Error Expired = Error.Validation(
            "OTP.Expired",
            "The OTP code has expired. Please request a new one.");

        public static readonly Error AlreadyUsed = Error.Conflict(
            "OTP.AlreadyUsed",
            "This OTP has already been used.");

        public static readonly Error TooManyAttempts = Error.Validation(
            "OTP.TooManyAttempts",
            "Too many failed attempts. Your account is temporarily locked for security.");

        public static readonly Error InvalidCode = Error.Validation(
            "OTP.InvalidCode",
            "The code you entered is incorrect.");
    }
}
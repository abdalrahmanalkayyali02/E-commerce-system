using Common.Result;

namespace ECommerce.Application.Feature.IAC.ApplicationError
{
    public static class OtpAppErrors
    {
        public static readonly Error Required = Error.Validation(
            "OTP.Required",
            "OTP code is mandatory.");

        public static readonly Error InvalidFormat = Error.Validation(
            "OTP.InvalidFormat",
            "The OTP must be a 6-digit numeric code.");

        public static readonly Error InvalidCode = Error.Validation(
            "OTP.InvalidCode",
            "The code you entered is incorrect.");

        public static readonly Error NotFound = Error.NotFound(
            "OTP.NotFound",
            "No active OTP found for this account.");
    }
}
using Common.Result;

namespace ECommerce.Domain.Modules.IAC.DomainError
{
    public static class DateOfBirthErrors
    {
        public static readonly Error InvalidFormat = Error.Validation(
            "DOB.InvalidFormat",
            "Date of birth must be in YYYY-MM-DD format.");

        public static readonly Error InvalidAge = Error.Validation(
            "DOB.InvalidAge",
            "Age must be between 18 and 150 years.");

        public static readonly Error FutureDate = Error.Validation(
            "DOB.FutureDate",
            "Date of birth cannot be in the future.");
    }
}
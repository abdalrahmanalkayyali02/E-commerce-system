using Common.Result;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class UnitError
    {
        public static readonly Error Required = Error.Validation(
            "Unit.Required",
            "Measurement unit is mandatory for this attribute.");

        public static readonly Error InvalidUnit = Error.Validation(
            "Unit.InvalidUnit",
            "The provided unit is not supported by our catalog system.");

        public static readonly Error InvalidLength = Error.Validation(
            "Unit.InvalidLength",
            "Unit name cannot exceed 10 characters.");
    }
}
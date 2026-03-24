using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record Unit
    {
        private static readonly HashSet<string> AllowedUnits = new()
        { "kg", "g", "cm", "m", "ml", "l", "inch", "pcs" };

        public string Value { get; init; }

        private Unit(string value) => Value = value;

        public static Result<Unit> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return UnitError.Required;

            var processedUnit = value.Trim().ToLower();

            if (!AllowedUnits.Contains(processedUnit))
                return UnitError.InvalidUnit;

            return Result<Unit>.Success(new Unit(processedUnit));
        }

        public static Unit Reconstruct(string value) => new Unit(value);
    }
}
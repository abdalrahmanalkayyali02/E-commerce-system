using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record AttributeValue
    {
        private static readonly Regex ValidPattern = new(@"^[a-zA-Z0-9]+( [a-zA-Z0-9]+)*$", RegexOptions.Compiled);

        public string Value { get; init; }

        private AttributeValue(string value) => Value = value;

        public static Result<AttributeValue> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return AttributeValueError.Required; 
            }

            var processedValue = value.Trim();

            if (processedValue.Length > 100)
            {
                return AttributeValueError.InvalidLength;
            }

            if (!ValidPattern.IsMatch(processedValue))
            {
                return AttributeValueError.InvalidFormat;
            }

            return Result<AttributeValue>.Success(new AttributeValue(processedValue));
        }

        public static AttributeValue Reconstruct(string value) => new AttributeValue(value);

        public override string ToString() => Value;
    }
}
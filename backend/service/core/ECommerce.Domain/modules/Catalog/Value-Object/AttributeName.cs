using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Catalog.Value_Object
{
    public sealed record AttributeName
    {
        private static readonly Regex SingleSpaceRegex = new(@"^[a-zA-Z0-9]+( [a-zA-Z0-9]+)*$", RegexOptions.Compiled);

        public string Value { get; init; }

        private AttributeName(string value) => Value = value;


        public static Result<AttributeName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return AttributeNameError.Required;
            }

            var processedValue = value.Trim();

            if (processedValue.Length > 50)
            {
                return AttributeNameError.InvalidLength;
            }

            if (!SingleSpaceRegex.IsMatch(processedValue))
            {
                return AttributeNameError.InvalidFormat;
            }

            return Result<AttributeName>.Success(new AttributeName(processedValue));
        }

        public static AttributeName Reconstruct(string value)
        {
            return new AttributeName(value);
        }

        public override string ToString() => Value;
    }
}
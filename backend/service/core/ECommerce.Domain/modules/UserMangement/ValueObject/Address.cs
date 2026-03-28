using Common.Impl.Result;
using ECommerce.Domain.modules.UserMangement.DomainError;

namespace ECommerce.Domain.modules.UserMangement.ValueObject
{
    public sealed record Address
    {
        public string Value { get; init; }

        private Address(string value)
        {
            Value = value;
        }

        public static Result<Address> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<Address>.Failure(AddressErrors.Required);
            }

            if (value.Length < 5 || value.Length > 150)
            {
                return Result<Address>.Failure(AddressErrors.InvalidLength);
            }

            return Result<Address>.Success(new Address(value));
        }

        public static Address Reconstruct(string value) => new Address(value);

        public override string ToString() => Value;
    }
}

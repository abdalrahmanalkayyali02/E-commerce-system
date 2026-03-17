using ECommerce.Domain.modules.IAC.ValueObject;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ECommerce.Infrastructure")]

namespace ECommerce.Domain.modules.IAC.Entity
{
    public class CustomerEntity
    {
        public Guid CustomrID { get; internal set; }
        public Address Address { get; internal set; }

        public DateTime CreateAt { get; internal set; }
        public DateTime UpdateAt { get; internal set; }

        private CustomerEntity() { }

        public CustomerEntity(Guid customrID, Address address, DateTime CreateAt, DateTime updateAt)
        {
            CustomrID = customrID;
            Address = address;
            this.CreateAt = CreateAt;
            this.UpdateAt = updateAt;
        }

        public static CustomerEntity Create(Guid userId, string rawAddress)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID is required to create a customer.");

            var addressVo = Address.Create(rawAddress);

            return new CustomerEntity(userId, addressVo,DateTime.Now,DateTime.Now);
        }


        public void UpdateAddress(string newRawAddress)
        {
            var newAddresValue = Address.Create(newRawAddress);

            if (Address.Value == newAddresValue.Value)
                return;

            Address = newAddresValue;
            this.UpdateAt = DateTime.UtcNow;
        }

    }
}
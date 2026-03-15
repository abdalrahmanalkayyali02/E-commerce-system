using IAC.Domain.Entity;
using IAC.Domain.Value_Object;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ECommerce.Infrastructure")]

namespace IAC.Domain.AggregateRoot
{
    public class CustomerAggregate
    {
        public Guid CustomrID { get; internal set; }
        public Address Address { get; internal set; }

        public DateTime CreateAt { get; internal set; }
        public DateTime UpdateAt { get; internal set; }

        private CustomerAggregate() { }

        public CustomerAggregate(Guid customrID, Address address, DateTime CreateAt, DateTime updateAt)
        {
            CustomrID = customrID;
            Address = address;
            this.CreateAt = CreateAt;
            this.UpdateAt = updateAt;
        }

        public static CustomerAggregate Create(Guid userId, string rawAddress)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID is required to create a customer.");

            var addressVo = Address.Create(rawAddress);

            return new CustomerAggregate(userId, addressVo,DateTime.Now,DateTime.Now);
        }


        public void UpdateAddress(string newRawAddress)
        {
            this.Address = Address.Create(newRawAddress);

            this.UpdateAt = DateTime.UtcNow;
        }

    }
}
using IAC.Domain.Entity;
using IAC.Domain.Value_Object;
using System;

namespace IAC.Domain.AggregateRoot
{
    public class CustomerAggregate
    {
        public Guid CustomrID { get; private set; }
        public Address Address { get; private set; }

        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        private CustomerAggregate() { }

        private CustomerAggregate(Guid customrID, Address address)
        {
            CustomrID = customrID;
            Address = address;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public static CustomerAggregate Create(Guid userId, string rawAddress)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID is required to create a customer.");

            var addressVo = Address.Create(rawAddress);

            return new CustomerAggregate(userId, addressVo);
        }


        public void UpdateAddress(string newRawAddress)
        {
            this.Address = Address.Create(newRawAddress);

            this.UpdateAt = DateTime.UtcNow;
        }

    }
}
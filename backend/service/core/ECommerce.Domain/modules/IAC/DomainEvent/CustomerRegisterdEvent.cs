using ECommerce.Domain.modules.IAC.ValueObject;
using MediatR;

namespace ECommerce.Domain.modules.IAC.DomainEvent
{
    public class CustomerRegisterdEvent : INotification 
    {
        public Guid userId { get; }
        public Name userName { get; }
        public Email userEmail { get; }
        public OTP otp { get; }
    }
}

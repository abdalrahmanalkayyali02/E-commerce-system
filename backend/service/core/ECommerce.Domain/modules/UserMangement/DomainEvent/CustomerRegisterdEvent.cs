using ECommerce.Domain.modules.UserMangement.ValueObject;
using MediatR;

namespace ECommerce.Domain.modules.UserMangement.DomainEvent
{
    public class CustomerRegisterdEvent : INotification 
    {
        public Guid userId { get; }
        public Name userName { get; }
        public Email userEmail { get; }
        public OTP otp { get; }
    }
}

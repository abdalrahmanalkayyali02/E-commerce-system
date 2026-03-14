using IAC.Domain.Value_Object;
using MediatR;

namespace IAC.Domain.Event
{
    public class CustomerRegisterdEvent : INotification 
    {
        public Guid userId { get; }
        public Name userName { get; }
        public Email userEmail { get; }
        public OTP otp { get; }
    }
}

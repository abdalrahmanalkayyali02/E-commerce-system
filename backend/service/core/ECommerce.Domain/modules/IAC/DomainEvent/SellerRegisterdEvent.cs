using ECommerce.Domain.modules.IAC.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.DomainEvent
{
    public class SellerRegisterdEvent : INotification
    {
        public Guid userID { get; private set; }
        public Email email { get; private set; }
        public Name userName { get; private set; }

        public string verifiedDocument { get; private set; }
        public OTP otp { get; }


    }
}

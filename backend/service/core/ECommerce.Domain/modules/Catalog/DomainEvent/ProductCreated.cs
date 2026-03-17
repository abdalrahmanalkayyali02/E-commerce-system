using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ECommerce.Domain.modules.Catalog.DomainEvent
{
    public class ProductCreated : INotification
    {
        public Guid ProductID { get; }
        public Guid catogryID { get; }

       // will add other details later
    }
}

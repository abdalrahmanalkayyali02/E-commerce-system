using ECommerce.Infrastructure.Persistence.Model;
using IAC.Domain.AggregateRoot;
using IAC.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Mapper
{
    public static class CustomerMapper
    {
        // Domain => Persistence
        public static CustomerDataModel FromDomainToPersistence(CustomerAggregate customer)
        {
            var customerDataModel = new CustomerDataModel();
            customerDataModel.Address = customer.Address.Value;
            customerDataModel.CustomrID = customer.CustomrID;
            customerDataModel.CreateAt = customer.CreateAt;
            customerDataModel.UpdateAt = customer.UpdateAt;

            return customerDataModel;
        }

        public static CustomerAggregate FromPersistenceToDomain(CustomerDataModel model)
        {
            var customer = new CustomerAggregate
                (
                    customrID: model.CustomrID,
                    address: Address.Create(model.Address),
                    CreateAt :model.CreateAt,
                    updateAt: model.UpdateAt
                );

            return customer;
        }


    }
}

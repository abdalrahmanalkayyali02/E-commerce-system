using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Mapper
{
    public static class CustomerMapper
    {
        // Domain => Persistence
        public static CustomerDataModel FromDomainToPersistence(CustomerEntity customer)
        {
            var customerDataModel = new CustomerDataModel();
            customerDataModel.Address = customer.Address.Value;
            customerDataModel.CustomrID = customer.CustomrID;
            customerDataModel.CreateAt = customer.CreateAt;
            customerDataModel.UpdateAt = customer.UpdateAt;

            return customerDataModel;
        }

        public static CustomerEntity FromPersistenceToDomain(CustomerDataModel model)
        {
            var customer = new CustomerEntity
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

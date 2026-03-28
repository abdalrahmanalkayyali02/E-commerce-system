using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using ECommerce.Infrastructure.Persistence.Model.UserMangement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Mapper.UserMangement
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
                    address: Address.Reconstruct(model.Address),
                    CreateAt :model.CreateAt,
                    updateAt: model.UpdateAt
                );

            return customer;
        }


    }
}

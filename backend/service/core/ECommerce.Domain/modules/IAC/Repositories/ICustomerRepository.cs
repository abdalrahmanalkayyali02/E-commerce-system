using ECommerce.Domain.modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Repositories
{
    public interface ICustomerRepository
    {
        public Task AddAsync(CustomerEntity entity,CancellationToken cancellation= default);
        public void Update(CustomerEntity entity, CancellationToken cancellation = default);


    }
}

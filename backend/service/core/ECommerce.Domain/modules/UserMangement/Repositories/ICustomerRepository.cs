using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.UserMangement.Repositories
{
    public interface ICustomerRepository
    {
        public Task AddAsync(CustomerEntity entity,CancellationToken cancellation= default);
        public void Update(CustomerEntity entity, CancellationToken cancellation = default);
        public Task<CustomerEntity?> GetEntityWithSpec(ISpecification<CustomerEntity> spec, CancellationToken cancellationToken = default);
        Task<CustomerEntity?> GetUserByID(Guid id, CancellationToken ct = default);

    }
}

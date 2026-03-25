using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.Repositories;
using ECommerce.Infrastructure.Persistence.Mapper.IAC;
using ECommerce.Infrastructure.Persistence.Model.IAC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.IAC
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(CustomerEntity entity, CancellationToken cancellation = default)
        {
            var model = CustomerMapper.FromDomainToPersistence(entity);

            await _context.Customers.AddAsync(model, cancellation);
        }

        public async Task<CustomerEntity?> GetEntityWithSpec(ISpecification<CustomerEntity> spec, CancellationToken cancellationToken = default)
        {
            IQueryable<CustomerDataModel> query = _context.Customers.AsNoTracking();

            var evaluatedQuery = query.Select(m => CustomerMapper.FromPersistenceToDomain(m));

            var finalQuery = SpecificationEvaluator<CustomerEntity>.GetQuery(evaluatedQuery, spec);

            return await finalQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public void Update(CustomerEntity entity, CancellationToken cancellation = default)
        {
            var model = CustomerMapper.FromDomainToPersistence(entity);
            _context.Customers.Update(model);
        }
    }
}

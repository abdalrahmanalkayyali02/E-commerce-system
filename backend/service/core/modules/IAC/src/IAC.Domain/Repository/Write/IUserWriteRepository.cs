using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Write
{
    public interface IUserWriteRepository
    {
        public Task AddAsync(UserAggregate user);
        public Task UpdateAsync(UserAggregate user);
        public Task DeleteAsync(Guid id);
    }
}

using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Write
{
    public interface IUserWriteRepository
    {
        public Task AddAsync(UserAggregate user,CancellationToken concellation = default);
        public Task UpdateAsync(UserAggregate user, CancellationToken concellation = default);
        public Task DeleteAsync(Guid i, CancellationToken concellation = default);
    }
}

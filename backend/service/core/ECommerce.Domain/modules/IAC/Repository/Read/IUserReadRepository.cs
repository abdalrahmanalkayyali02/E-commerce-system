using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Read
{
    public  interface IUserReadRepository
    {
        public Task<UserAggregate?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
        public Task<UserAggregate?> GetUserByUsername(string username, CancellationToken cancellationToken = default);

        public Task<UserAggregate?> GetUserById(Guid id, CancellationToken cancellationToken = default);

        public Task <UserAggregate?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);


    }
}

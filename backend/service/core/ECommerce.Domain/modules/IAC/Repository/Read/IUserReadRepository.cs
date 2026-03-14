using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Read
{
    public  interface IUserReadRepository
    {
        public Task<UserAggregate?> GetUserByEmail(string email);
        public Task<UserAggregate?> GetUserByUsername(string username);

        public Task<UserAggregate?> GetUserById(Guid id);

        public Task <UserAggregate?> GetUserByPhoneNumber(string phoneNumber);


    }
}

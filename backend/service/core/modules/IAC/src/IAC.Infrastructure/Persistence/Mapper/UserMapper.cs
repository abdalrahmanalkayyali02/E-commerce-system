using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace IAC.Infrastructure.Persistence.Mapper
{
    public static class UserMapper
    {
        // to convert form aggregate to model
        public static UserModel ToPersistence(UserAggregate user) { }

        public static UserModel ToDomain(UserAggregate user) { }
    }
}

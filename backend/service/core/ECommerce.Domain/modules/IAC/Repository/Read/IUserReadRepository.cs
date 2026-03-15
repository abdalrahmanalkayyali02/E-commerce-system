using IAC.Domain.AggregateRoot;
using Common.Abstraction.Reposotries;

namespace IAC.Domain.Repository.Read
{
    public  interface IUserReadRepository : IReadReposotries<UserAggregate>
    {
        public Task<UserAggregate?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
        public Task<UserAggregate?> GetUserByUsername(string username, CancellationToken cancellationToken = default);
        public Task <UserAggregate?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);

    }
}

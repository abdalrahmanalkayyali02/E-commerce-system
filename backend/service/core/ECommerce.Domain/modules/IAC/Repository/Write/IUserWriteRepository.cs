using Common.Abstraction.Reposotries;
using IAC.Domain.AggregateRoot;

namespace IAC.Domain.Repository.Write
{
    public interface IUserWriteRepository : IWriteReposotries<UserAggregate> { }

}

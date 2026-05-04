using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement.Strategy;

public interface IProfileStrategy
{
    UserType UserType { get; }
    Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct);

    Task<Result<object>> UpdateProfile(object data, CancellationToken ct);
}
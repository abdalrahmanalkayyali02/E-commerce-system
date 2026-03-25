using Common.Enum;
using Common.Impl.Result;
using Common.Result;

namespace ECommerce.Application.Feature.IAC.User.GetProfile.Strategy;

public class ProfileStrategyContext
{
    private readonly IEnumerable<IProfileStrategy> _strategies;

    public ProfileStrategyContext(IEnumerable<IProfileStrategy> strategies)
    {
        _strategies = strategies;
    }

    public Result<IProfileStrategy> GetStrategy(UserType role)
    {
        var strategy = _strategies.FirstOrDefault(s => s.userType == role);

        if (strategy == null)
        {
            return Result<IProfileStrategy>.Failure(Error.NotFound("strategy.NotFound","Strategy not found for this user type."));
        }

        return Result<IProfileStrategy>.Success(strategy);
    }
}
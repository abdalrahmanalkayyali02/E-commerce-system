using WebApplication1.Repository.Interface;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement.Strategy;

public class ProfileStrategyContext
{
    private readonly IEnumerable<IProfileStrategy> _strategies;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileStrategyContext(IEnumerable<IProfileStrategy> strategies, IUnitOfWork unitOfWork)
    {
        _strategies = strategies;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IProfileStrategy>> GetStrategy(Guid userId)
    {
        var userExist = await _unitOfWork.Users.GetUserById(userId);

        if (userExist == null)
        {
            return Result<IProfileStrategy>.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        var role = userExist.Role;

        var strategy = _strategies.FirstOrDefault(s => s.UserType == role);

        if (strategy == null)
        {
            return Result<IProfileStrategy>.Failure(
                Error.NotFound("Strategy.NotFound", $"No profile strategy registered for {role}.")
            );
        }

        return Result<IProfileStrategy>.Success(strategy);
    }
    
    public async Task<Result<object>> UpdateProfileAsync(Guid userId, object command, CancellationToken ct)
    {
        var user = await _unitOfWork.Users.GetUserById(userId, ct);
        if (user is null)
        {
            return Result<object>.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        var strategyResult = await GetStrategy(user.id);

        if (strategyResult.IsFailure)
        {
            return Result<object>.Failure(strategyResult.Error);
        }

        var result = await strategyResult.Value?.UpdateProfile(command, ct);

        if (result.IsFailure)
            return result;

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<object>.Success(true);
    }
}
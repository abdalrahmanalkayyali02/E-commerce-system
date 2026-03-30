using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;

namespace ECommerce.Application.Feature.userMangement.User.Profile
{
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
            var userExist = await _unitOfWork.Users.GetUserByID(userId);

            if (userExist == null)
            {
                return Result<IProfileStrategy>.Failure(UserIdAppError.NotFound);
            }

            var role = userExist.userType;

            var strategy = _strategies.FirstOrDefault(s => s.userType == role);

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
            var user = await _unitOfWork.Users.GetUserByID(userId, ct);
            if (user is null)
            {
                return Result<object>.Failure(Error.NotFound("User.NotFound", "User not found."));
            }

            var strategyResult = await GetStrategy(user.Id);

            if (strategyResult.IsError)
            {
                return Result<object>.Failure(strategyResult.Errors);
            }

            var result = await strategyResult.Value.UpdateProfile(command, ct);

            if (result.IsError)
                return result;

            await _unitOfWork.SaveChangesAsync(ct);

            return Result<object>.Success(true);
        }
    }
}
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Feature.userMangement.User.Profile;
using ECommerce.Application.Feature.userMangement.User.Profile.GetProfile.Queries;
using MediatR;

namespace ECommerce.Application.Feature.userMangement.User.Profile.GetProfile.Handler
{
    public class GetProfileByIdHandler : IRequestHandler<GetUserByIdQueries, Result<object>>
    {
        private readonly ProfileStrategyContext _strategyContext;

        public GetProfileByIdHandler(ProfileStrategyContext strategyContext)
        {
            _strategyContext = strategyContext;
        }

        public async Task<Result<object>> Handle(GetUserByIdQueries request, CancellationToken ct)
        {
            // 1. Get the strategy from the context
            var strategyResult = _strategyContext.GetStrategy(request.userType);

            if (strategyResult.IsError)
            {
                return Result<object>.Failure(strategyResult.Errors.FirstOrDefault());
            }

            var result = await strategyResult.Value.GetProfileAsync(request.id, ct);

            return result;
        }
    }
}
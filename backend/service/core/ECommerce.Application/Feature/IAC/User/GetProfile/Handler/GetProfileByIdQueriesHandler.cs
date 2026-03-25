using Common.DTOs.IAC.User;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Feature.IAC.User.GetProfile.Queries;
using ECommerce.Application.Feature.IAC.User.GetProfile.Strategy;
using MediatR;

namespace ECommerce.Application.Feature.IAC.User.GetProfile.Handler
{
    public class GetProfileByIdHandler : IRequestHandler<GetUserByIdQueries, Result<UserResponse>>
    {
        private readonly ProfileStrategyContext _strategyContext;

        public GetProfileByIdHandler(ProfileStrategyContext strategyContext)
        {
            _strategyContext = strategyContext;
        }

        public async Task<Result<UserResponse>> Handle(GetUserByIdQueries request, CancellationToken ct)
        {
    
            var strategy = _strategyContext.GetStrategy(request.userType);

      
            var result = await strategy.Value.GetProfileAsync(request.id, ct);

            return result;
        }
    }
}
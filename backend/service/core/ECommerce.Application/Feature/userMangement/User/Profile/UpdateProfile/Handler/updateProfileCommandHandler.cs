using Common.DTOs.IAC.User;
using Common.Impl.Result;
using ECommerce.Application.Feature.userMangement.User.Profile;
using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using MediatR;

namespace ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Handler
{
    public class updateProfileCommandHandler :
        IRequestHandler<userProfileCommand, Result<UpdateUserProfileResponse>>,
        IRequestHandler<customerProfileCommand, Result<UpdateUserProfileResponse>>,
        IRequestHandler<sellerProfileCommand, Result<UpdateUserProfileResponse>>
    {
        private readonly ProfileStrategyContext _strategyContext;

        public updateProfileCommandHandler(ProfileStrategyContext strategyContext)
        {
            _strategyContext = strategyContext;
        }

        public async Task<Result<UpdateUserProfileResponse>> Handle(userProfileCommand request, CancellationToken ct)
            => await ProcessRequest(request.userID, request, ct);

        public async Task<Result<UpdateUserProfileResponse>> Handle(customerProfileCommand request, CancellationToken ct)
            => await ProcessRequest(request.userID, request, ct);

        public async Task<Result<UpdateUserProfileResponse>> Handle(sellerProfileCommand request, CancellationToken ct)
            => await ProcessRequest(request.userID, request, ct);

        private async Task<Result<UpdateUserProfileResponse>> ProcessRequest(Guid userId, object command, CancellationToken ct)
        {
            var result = await _strategyContext.UpdateProfileAsync(userId, command, ct);

            if (result.IsError)
            {
                return Result<UpdateUserProfileResponse>.Failure(result.Errors);
            }

            return Result<UpdateUserProfileResponse>.Success(
                new UpdateUserProfileResponse("Profile updated successfully.", userId)
            );
        }
    }
}
using WebApplication1.DTOs;
using WebApplication1.Service.Impl.UserMagement.Strategy;
using WebApplication1.Service.Interface.UserMangement;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement;

public class ProfileService : IProfileService
{
    private readonly ProfileStrategyContext _strategyContext;

    public ProfileService(ProfileStrategyContext strategyContext)
    {
        _strategyContext = strategyContext;
    }

    public async Task<Result<UpdateUserProfileResponse>> UpdateUserProfile(UpdateUserProfileDtOs request, CancellationToken ct)
        => await ProcessRequest(request.UserId, request, ct);

    public async Task<Result<UpdateUserProfileResponse>> UpdateCustomerProfile(UpdateCustomerProfileDtOs request, CancellationToken ct)
        => await ProcessRequest(request.UserId, request, ct);

    public async Task<Result<UpdateUserProfileResponse>> UpdateSellerProfile(UpdateSellerProfileDtOs request, CancellationToken ct)
        => await ProcessRequest(request.UserId, request, ct);

    private async Task<Result<UpdateUserProfileResponse>> ProcessRequest(Guid userId, object dto, CancellationToken ct)
    {
        var result = await _strategyContext.UpdateProfileAsync(userId, dto, ct);

        if (result.IsFailure)
        {
            return Result<UpdateUserProfileResponse>.Failure(result.Error);
        }

        return Result<UpdateUserProfileResponse>.Success(
            new UpdateUserProfileResponse("Profile updated successfully.", userId)
        );
    }
}
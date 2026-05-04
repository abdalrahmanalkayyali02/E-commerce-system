using WebApplication1.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface.UserMangement;

public interface IProfileService
{
    Task<Result<UpdateUserProfileResponse>> UpdateUserProfile(UpdateUserProfileDtOs request, CancellationToken ct);
    Task<Result<UpdateUserProfileResponse>> UpdateCustomerProfile(UpdateCustomerProfileDtOs request, CancellationToken ct);
    Task<Result<UpdateUserProfileResponse>> UpdateSellerProfile(UpdateSellerProfileDtOs request, CancellationToken ct);
}
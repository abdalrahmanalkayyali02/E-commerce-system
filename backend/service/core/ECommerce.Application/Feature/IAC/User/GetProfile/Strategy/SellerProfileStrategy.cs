using Common.DTOs.IAC.User;
using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Domain.modules.IAC.Specfication;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Feature.IAC.User.GetProfile.Strategy
{
    public class SellerProfileStrategy : IProfileStrategy
    {
        private readonly IUnitOfWork _unitOfWork;

        public SellerProfileStrategy(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserType userType => UserType.Seller;

        public async Task<Result<UserResponse>> GetProfileAsync(Guid id, CancellationToken ct)
        {
            // 1. Fetch the Base User Identity
            var userSpec = new UserByIdSpecfication(id);
            var userEntity = await _unitOfWork.Users.GetEntityWithSpec(userSpec, ct);

            if (userEntity is null)
            {
                return Result<UserResponse>.Failure(Error.NotFound("User.NotFound", "The user record does not exist."));
            }

            var baseUser = new UserResponse(
                userEntity.FirstName.Value,
                userEntity.LastName.Value,
                userEntity.UserName.Value,
                userEntity.Email.Value,
                userEntity.PhoneNumber.Value,
                userEntity.DateOfBirth.Value,
                userEntity.ProfilePhoto,
                userEntity.userType,
                userEntity.AccountStatus,
                userEntity.CreatedAt
            );

            var sellerSpec = new SellerByIdSpecfication(userEntity.Id); 
            var sellerEntity = await _unitOfWork.Seller.GetEntityWithSpec(sellerSpec, ct);

            if (sellerEntity is null)
            {
                return Result<UserResponse>.Failure(Error.NotFound("Seller.NotFound", "Seller profile data is missing."));
            }

  


            return Result<UserResponse>.Success(new SellerResponse(
                baseUser,
                sellerEntity.shopName,
                sellerEntity.shopPhoto,
                sellerEntity.address.Value,
                sellerEntity.isVerifiedByAdmin,
                sellerEntity.verfiedSellerDocument,
                sellerEntity.verfiedShopDocument
            ));
        }
    }
}
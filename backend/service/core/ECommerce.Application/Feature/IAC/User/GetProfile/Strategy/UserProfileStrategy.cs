using Common.DTOs.IAC.User;
using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Domain.modules.IAC.Specfication;

namespace ECommerce.Application.Feature.IAC.User.GetProfile.Strategy
{
    public class UserProfileStrategy : IProfileStrategy
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserType userType => UserType.Admin;

        public UserProfileStrategy(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserResponse>> GetProfileAsync(Guid id, CancellationToken ct)
        {
            var userSpec = new UserByIdSpecfication(id);
            var userEntity = await _unitOfWork.Users.GetEntityWithSpec(userSpec, ct);

            if (userEntity is null)
            {
                return Result<UserResponse>.Failure(
                    Error.NotFound("User.NotFound", "User record not found.")
                );
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

            return Result<UserResponse>.Success(baseUser);
        }
    }
}
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
    public class CustomerProfileStrategy : IProfileStrategy
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerProfileStrategy(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Identifying this strategy for the Context
        public UserType userType => UserType.Customer;

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

            var customerSpec = new CustomerrByIdSpecfication(id);
            var customerEntity = await _unitOfWork.Customer.GetEntityWithSpec(customerSpec, ct);

            if (customerEntity is null)
            {
 
                return Result<UserResponse>.Failure(Error.NotFound("Customer.NotFound", "Customer profile data is missing."));
            }

            return Result<UserResponse>.Success(new CustomerResponse(
                baseUser,
                customerEntity.Address.Value
            ));
        }
    }
}
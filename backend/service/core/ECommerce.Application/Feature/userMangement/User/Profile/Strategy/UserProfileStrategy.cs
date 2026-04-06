using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.userMangement.User.Profile;
using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;

namespace ECommerce.Application.Feature.userMangement.User.Profile.Strategy
{
    public class UserProfileStrategy : IProfileStrategy
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<userProfileCommand> _validator;

        public UserType userType => UserType.Admin;

        public UserProfileStrategy(IUnitOfWork unitOfWork, IValidator<userProfileCommand> _validator)
        {
            _unitOfWork = unitOfWork;
            this._validator = _validator;
        }

        public async Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct)
        {

            
            var userEntity = await _unitOfWork.Users.GetUserByID(id, ct);

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

            return Result<object>.Success(baseUser);
        }

        public async Task<Result<object>> UpdateProfile(object data , CancellationToken ct)
        {

            if (data is not userProfileCommand command)
            {
                return Result<object>.Failure(Error.Unexpected("Invalid.CommandType", "Invalid command type for the user profile strategy."));
            }

            var validationResult = await _validator.ValidateAsync(command, ct);
            if (!validationResult.IsValid)
            {
                return Result<object>.Failure(
                    Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
            }

            var userEntity = await _unitOfWork.Users.GetUserByID(command.userID, ct);

            if (userEntity is null)
            {
                return Result<object>.Failure(UserIdAppError.NotFound);
            }

            if (!string.IsNullOrEmpty(command.profilePhoto))
            {
                userEntity.UpdateProfilePhoto(command.profilePhoto);
            }

            if (!string.IsNullOrEmpty(command.FirstName))
            {
                var firstNameResult = Name.FromStrict(command.FirstName);
                if (firstNameResult.IsError) return Result<object>.Failure(firstNameResult.Errors);

                userEntity.UpdateFirstName(command.FirstName);
            }

            if (!string.IsNullOrEmpty(command.LastName))
            {
                var lastNameResult = Name.FromStrict(command.LastName);
                if (lastNameResult.IsError) return Result<object>.Failure(lastNameResult.Errors);

                userEntity.UpdateLastName(command.LastName);
            }

            if (!string.IsNullOrEmpty(command.phoneNumber))
            {
                var phoneResult = PhoneNumber.From(command.phoneNumber);
                
                if (phoneResult.IsError) 
                    return Result<object>.Failure(phoneResult.Errors);
                userEntity.UpdatePhoneNumber(command.phoneNumber);
            }

            _unitOfWork.Users.Update(userEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success(true);
        }
    }
}
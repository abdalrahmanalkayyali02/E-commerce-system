using Common.DTOs.IAC.User;
using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.userMangement.ApplicationError;
using ECommerce.Application.Feature.userMangement.User.Profile;
using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Feature.userMangement.User.Profile.Strategy
{
    public class SellerProfileStrategy : IProfileStrategy
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<sellerProfileCommand> _validator;

        public SellerProfileStrategy(IUnitOfWork unitOfWork, IValidator<sellerProfileCommand> _validator)
        {
            _unitOfWork = unitOfWork;
            this._validator = _validator;
        }


        public UserType userType => UserType.Seller;

     
        public async Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct)
        {
            // 1. Fetch the Base User
            var userEntity = await _unitOfWork.Users.GetUserByID(id, ct);

            if (userEntity is null)
            {
                return Result<object>.Failure(Error.NotFound("User.NotFound", "The user record does not exist."));
            }

            // 2. Fetch the Seller specific data
            var sellerEntity = await _unitOfWork.Seller.GetSellerWithID(id, ct);

            if (sellerEntity is null)
            {
                return Result<object>.Failure(Error.NotFound("Seller.NotFound", "Seller profile data is missing."));
            }

            var response = new SellerResponse(
                userEntity.FirstName.Value,
                userEntity.LastName.Value,
                userEntity.UserName.Value,
                userEntity.Email.Value,
                userEntity.PhoneNumber.Value,
                userEntity.DateOfBirth.Value,
                userEntity.ProfilePhoto,
                userEntity.userType,
                userEntity.AccountStatus,
                userEntity.CreatedAt,
                sellerEntity.shopName,
                sellerEntity.shopPhoto,
                sellerEntity.address.Value,
                sellerEntity.isVerifiedByAdmin,
                sellerEntity.verfiedSellerDocument,
                sellerEntity.verfiedShopDocument
            );

            return Result<object>.Success(response);
        }

        public async Task<Result<object>> UpdateProfile(object data, CancellationToken ct)
        {
            if (data is not sellerProfileCommand command)
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


            var sellerEntity = await _unitOfWork.Seller.GetSellerWithID(command.userID, ct);

            if (sellerEntity is null)
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

            if (!string.IsNullOrEmpty(command.address))
            {
                var address = Address.Create(command.address);

                if (address.IsError)
                    return Result<object>.Failure(address.Errors);
                sellerEntity.updateShopAddress(address.Value);
            }

            if (!string.IsNullOrEmpty(command.verifiedSellerDocument))
            {

                sellerEntity.updateVerfiedSellerDocument(command.verifiedSellerDocument);
            }


             _unitOfWork.Users.Update(userEntity);
            _unitOfWork.Seller.Update(sellerEntity);
            await _unitOfWork.SaveChangesAsync(ct);
            return Result<object>.Success(true);

        }
    }
}
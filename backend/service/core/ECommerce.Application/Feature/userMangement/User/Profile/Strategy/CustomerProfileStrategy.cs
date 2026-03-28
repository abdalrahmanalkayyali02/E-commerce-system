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

public class CustomerProfileStrategy : IProfileStrategy
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<customerProfileCommand> _validator;

    public CustomerProfileStrategy(IUnitOfWork unitOfWork, IValidator<customerProfileCommand> _validator)
    {
        _unitOfWork = unitOfWork;
        this._validator = _validator;
    }

    public UserType userType => UserType.Customer;

    public async Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct)
    {
        var userEntity = await _unitOfWork.Users.GetUserByID(id, ct);

        if (userEntity is null)
        {
            return Result<object>.Failure(Error.NotFound("User.NotFound", "The user record does not exist."));
        }

        var customerEntity = await _unitOfWork.Customer.GetUserByID(userEntity.Id, ct);

        if (customerEntity is null)
        {
            return Result<object>.Failure(Error.NotFound("Customer.NotFound", "Customer profile data is missing."));
        }

        var response = new CustomerResponse(
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
            customerEntity.Address.Value 
        );

        return Result<object>.Success(response);
    }

    public async Task<Result<object>> UpdateProfile(object data, CancellationToken ct)
    {
        if (data is not customerProfileCommand command)
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


        var customerEntity = await _unitOfWork.Customer.GetUserByID(command.userID, ct);

        if (customerEntity is null)
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

        // now for address
        if (!string.IsNullOrEmpty(command.address))
        {
            var address = Address.Create(command.address);

            if (address.IsError)
                return Result<object>.Failure(address.Errors);
            customerEntity.UpdateAddress(command.address);
        }

        _unitOfWork.Users.Update(userEntity);
        _unitOfWork.Customer.Update(customerEntity);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result<object>.Success(true);
    }
}
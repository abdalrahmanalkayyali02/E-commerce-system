using FluentValidation;
using WebApplication1.DTOs;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement.Strategy.ConcreateStrategy;

public class CustomerProfileStrategy : IProfileStrategy
{
    public UserType UserType => UserType.Customer;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateCustomerProfileDtOs> _validator;
    private readonly IFileStorageService _fileStorageService;

    public CustomerProfileStrategy(IUnitOfWork unitOfWork, IValidator<UpdateCustomerProfileDtOs> validator, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _fileStorageService = fileStorageService;
    }


    public async Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct)
    {
        var userEntity = await _unitOfWork.Users.GetUserById(id, ct);

        if (userEntity is null)
        {
            return Result<object>.Failure(Error.NotFound("User.NotFound", "The user record does not exist."));
        }

        var customerEntity = await _unitOfWork.Customer.GetUserById(userEntity.id, ct);

        if (customerEntity is null)
        {
            return Result<object>.Failure(Error.NotFound("Customer.NotFound", "Customer profile data is missing."));
        }

        var response = new CustomerDtOs(
            userEntity.FirstName,
            userEntity.LastName,
            userEntity.UserName,
            userEntity.Email,
            userEntity.phoneNumber,
            userEntity.DateOfBirth,
            userEntity.profilePhoto,
            userEntity.Role,
            userEntity.AccountStatus,
            userEntity.CreateAt,
            customerEntity.Address
        );

        return Result<object>.Success(response);
    }

    public async Task<Result<object>> UpdateProfile(object data, CancellationToken ct)
    {
        if (data is not UpdateCustomerProfileDtOs command)
        {
            return Result<object>.Failure(Error.Unexpected("Invalid.CommandType",
                "Invalid command type for the user profile strategy."));
        }

        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            return Result<object>.Failure(
                Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
        }

        var userEntity = await _unitOfWork.Users.GetUserById(command.UserId, ct);

        if (userEntity is null)
        {
            return Result<object>.Failure(Error.NotFound("User.NotFound", "User record not found."));
        }


        var customerEntity = await _unitOfWork.Customer.GetUserById(command.UserId, ct);

        if (customerEntity is null)
        {
            return Result<object>.Failure(Error.NotFound("Customer.NotFound", "Customer record not found."));
        }

        if (command.ProfilePhoto is { Length: > 0 })
        {
            var profilePhotoUrl = await UploadFile(command.ProfilePhoto);
            if (profilePhotoUrl != null)
            {
                userEntity.profilePhoto = profilePhotoUrl;
            }
        }

        if (!string.IsNullOrEmpty(command.FirstName))
        {

            userEntity.FirstName = command.FirstName;
        }

        if (!string.IsNullOrEmpty(command.LastName))
        {
            userEntity.LastName = command.LastName;
        }

        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            userEntity.phoneNumber = command.PhoneNumber;
        }

        // now for address
        if (!string.IsNullOrEmpty(command.Address))
        {
            customerEntity.Address = command.Address;
        }

        _unitOfWork.Users.Update(userEntity, ct);
        _unitOfWork.Customer.Update(customerEntity, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result<object>.Success(true);
    }
    
    private async Task<string?> UploadFile(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return null;

        await using var stream = file.OpenReadStream();

        if (stream.CanSeek)
        {
            stream.Position = 0;
        }

        return await _fileStorageService.UploadAsync(stream);
    }
}
    

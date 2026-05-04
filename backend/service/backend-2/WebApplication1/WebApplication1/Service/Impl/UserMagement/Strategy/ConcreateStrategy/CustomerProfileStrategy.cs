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
    private readonly IValidator<UpdateCustomerProfileDtOs> _customerValidator;
    private readonly IValidator<UpdateUserProfileDtOs> _baseValidator;
    private readonly IFileStorageService _fileStorageService;

    public CustomerProfileStrategy(
        IUnitOfWork unitOfWork, 
        IValidator<UpdateCustomerProfileDtOs> customerValidator, 
        IValidator<UpdateUserProfileDtOs> baseValidator,
        IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _customerValidator = customerValidator;
        _baseValidator = baseValidator;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct)
    {
        var userEntity = await _unitOfWork.Users.GetUserById(id, ct);
        if (userEntity is null) return Result<object>.Failure(Error.NotFound("User.NotFound", "User record missing."));

        var customerEntity = await _unitOfWork.Customer.GetUserById(userEntity.id, ct);
        if (customerEntity is null) return Result<object>.Failure(Error.NotFound("Customer.NotFound", "Customer data missing."));

        return Result<object>.Success(new CustomerDtOs(
            userEntity.FirstName, userEntity.LastName, userEntity.UserName, userEntity.Email,
            userEntity.phoneNumber, userEntity.DateOfBirth, userEntity.profilePhoto,
            userEntity.Role, userEntity.AccountStatus, userEntity.CreateAt, customerEntity.Address
        ));
    }

    public async Task<Result<object>> UpdateProfile(object data, CancellationToken ct)
    {
        // FIX: Polymorphic check allows base DTO to pass
        if (data is not UpdateUserProfileDtOs command)
        {
            return Result<object>.Failure(Error.Unexpected("Invalid.CommandType", "Data type mismatch."));
        }

        var userEntity = await _unitOfWork.Users.GetUserById(command.UserId, ct);
        if (userEntity is null) return Result<object>.Failure(Error.NotFound("User.NotFound", "User not found."));

        // Update Common Base Fields
        if (command.ProfilePhoto is { Length: > 0 })
            userEntity.profilePhoto = await UploadFile(command.ProfilePhoto);

        if (!string.IsNullOrEmpty(command.FirstName)) userEntity.FirstName = command.FirstName;
        if (!string.IsNullOrEmpty(command.LastName)) userEntity.LastName = command.LastName;
        if (!string.IsNullOrEmpty(command.PhoneNumber)) userEntity.phoneNumber = command.PhoneNumber;

        _unitOfWork.Users.Update(userEntity, ct);

        // Update Customer Specific Fields if provided
        if (data is UpdateCustomerProfileDtOs customerDto && !string.IsNullOrEmpty(customerDto.Address))
        {
            var customerEntity = await _unitOfWork.Customer.GetUserById(command.UserId, ct);
            if (customerEntity != null)
            {
                customerEntity.Address = customerDto.Address;
                _unitOfWork.Customer.Update(customerEntity, ct);
            }
        }

        return Result<object>.Success(true);
    }

    private async Task<string?> UploadFile(IFormFile? file)
    {
        if (file == null || file.Length == 0) return null;
        await using var stream = file.OpenReadStream();
        return await _fileStorageService.UploadAsync(stream);
    }
}
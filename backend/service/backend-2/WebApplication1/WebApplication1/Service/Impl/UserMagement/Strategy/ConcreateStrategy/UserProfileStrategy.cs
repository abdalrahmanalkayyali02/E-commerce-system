using FluentValidation;
using WebApplication1.DTOs;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement.Strategy.ConcreateStrategy;

public class UserProfileStrategy : IProfileStrategy
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateUserProfileDtOs> _validator;
    private readonly IFileStorageService _fileStorageService;

    // Based on your specific implementation for Admin/Base User roles
    public UserType UserType => UserType.Admin;

    public UserProfileStrategy(
        IUnitOfWork unitOfWork, 
        IValidator<UpdateUserProfileDtOs> validator, 
        IFileStorageService fileStorageService)
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
            return Result<object>.Failure(
                Error.NotFound("User.NotFound", "User record not found.")
            );
        }

        var baseUser = new UserDtOs(
            userEntity.FirstName,
            userEntity.LastName,
            userEntity.UserName,
            userEntity.Email,
            userEntity.phoneNumber,
            userEntity.DateOfBirth,
            userEntity.profilePhoto,
            userEntity.Role,
            userEntity.AccountStatus,
            userEntity.CreateAt
        );

        return Result<object>.Success(baseUser);
    }

    public async Task<Result<object>> UpdateProfile(object data, CancellationToken ct)
    {
        if (data is not UpdateUserProfileDtOs command)
        {
            return Result<object>.Failure(Error.Unexpected("Invalid.CommandType", "Data type mismatch."));
        }

        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            return Result<object>.Failure(
                Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage)
            );
        }

        var userEntity = await _unitOfWork.Users.GetUserById(command.UserId, ct);

        if (userEntity is null)
        {
            return Result<object>.Failure(
                Error.NotFound("User.NotFound", "User record not found.")
            );
        }

        // Handle Profile Photo Upload if a new file is provided
        if (command.ProfilePhoto is { Length: > 0 })
        {
            var profilePhotoUrl = await UploadFile(command.ProfilePhoto);
            if (profilePhotoUrl != null)
            {
                userEntity.profilePhoto = profilePhotoUrl;
            }
        }

        // Apply string updates only if values are provided
        if (!string.IsNullOrWhiteSpace(command.FirstName))
        {
            userEntity.FirstName = command.FirstName;
        }

        if (!string.IsNullOrWhiteSpace(command.LastName))
        {
            userEntity.LastName = command.LastName;
        }

        if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
        {
            userEntity.phoneNumber = command.PhoneNumber;
        }

        _unitOfWork.Users.Update(userEntity, ct);
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
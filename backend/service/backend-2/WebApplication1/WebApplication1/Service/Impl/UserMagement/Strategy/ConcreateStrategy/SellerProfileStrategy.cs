using FluentValidation;
using WebApplication1.DTOs;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement.Strategy.ConcreateStrategy;

public class SellerProfileStrategy : IProfileStrategy
{
    public UserType UserType => UserType.Seller;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateSellerProfileDtOs> _validator;
    private readonly IFileStorageService _fileStorageService;

    public SellerProfileStrategy(IUnitOfWork unitOfWork, IValidator<UpdateSellerProfileDtOs> validator, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<object>> UpdateProfile(object data, CancellationToken ct)
    {
        // FIX: Cast to base DTO to support updates from the generic endpoint
        if (data is not UpdateUserProfileDtOs command)
        {
            return Result<object>.Failure(Error.Unexpected("Invalid.CommandType", "Data type mismatch."));
        }

        var userEntity = await _unitOfWork.Users.GetUserById(command.UserId, ct);
        if (userEntity is null) return Result<object>.Failure(Error.NotFound("User.NotFound", "User missing."));

        if (command.ProfilePhoto is { Length: > 0 })
            userEntity.profilePhoto = await UploadFile(command.ProfilePhoto);

        if (!string.IsNullOrEmpty(command.FirstName)) userEntity.FirstName = command.FirstName;
        if (!string.IsNullOrEmpty(command.LastName)) userEntity.LastName = command.LastName;
        if (!string.IsNullOrEmpty(command.PhoneNumber)) userEntity.phoneNumber = command.PhoneNumber;

        if (data is UpdateSellerProfileDtOs sellerDto)
        {
            var sellerEntity = await _unitOfWork.Seller.GetSellerWithId(command.UserId, ct);
            if (sellerEntity != null)
            {
                if (!string.IsNullOrEmpty(sellerDto.Address)) sellerEntity.address = sellerDto.Address;
                _unitOfWork.Seller.Update(sellerEntity, ct);
            }
        }

        _unitOfWork.Users.Update(userEntity, ct);
        return Result<object>.Success(true);
    }

    private async Task<string?> UploadFile(IFormFile? file) { /* implementation */ return null; }
    public async Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct) { /* implementation */ return Result<object>.Success(null); }
}
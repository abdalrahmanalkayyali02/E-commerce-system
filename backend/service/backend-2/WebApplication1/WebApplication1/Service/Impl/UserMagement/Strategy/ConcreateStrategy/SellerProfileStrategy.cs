using FluentValidation;
using WebApplication1.DTOs;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement.Strategy.ConcreateStrategy;

public class SellerProfileStrategy : IProfileStrategy
{
    
    public UserType UserType  => UserType.Seller;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateSellerProfileDtOs> _validator;
    private readonly IFileStorageService _fileStorageService;
    
    public SellerProfileStrategy(IUnitOfWork unitOfWork, IValidator<UpdateSellerProfileDtOs> validator, IFileStorageService fileStorageService)
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

        // 2. Fetch the Seller-specific data
        var sellerEntity = await _unitOfWork.Seller.GetSellerWithId(id, ct);

        if (sellerEntity is null)
        {
            return Result<object>.Failure(Error.NotFound("Seller.NotFound", "Seller profile data is missing."));
        }

        var response = new SellerDtOs(
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
            sellerEntity.ShopName,
            sellerEntity.ShopPhoto,
            sellerEntity.address,
            sellerEntity.isVerifiedByAdmin,
            sellerEntity.verfiedSellerDocument,
            sellerEntity.verfiedShopDocument
        );

        return Result<object>.Success(response);
    }

    public async Task<Result<object>> UpdateProfile(object data, CancellationToken ct)
    {
           if (data is not UpdateSellerProfileDtOs command)
           {
               return Result<object>.Failure(Error.Unexpected(
                   "Invalid.CommandType", "Invalid command type for the user profile strategy."));
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


           var sellerEntity = await _unitOfWork.Seller.GetSellerWithId(command.UserId, ct);

           if (sellerEntity is null)
           {
               return Result<object>.Failure(Error.NotFound("Seller.NotFound", "Seller record not found."));
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

               userEntity.LastName = command.LastName;;
           }

           if (!string.IsNullOrEmpty(command.PhoneNumber))
           {
               userEntity.phoneNumber = command.PhoneNumber;;

           }

           if (!string.IsNullOrEmpty(command.Address))
           {
               userEntity.phoneNumber = command.Address;;
           }
           
           _unitOfWork.Users.Update(userEntity,ct);
           _unitOfWork.Seller.Update(sellerEntity,ct);
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
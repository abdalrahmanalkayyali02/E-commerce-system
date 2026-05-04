using FluentValidation;
using WebApplication1.Data.Model;
using WebApplication1.DTOs;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Service.Interface.UserMangement;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Helper;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Impl.UserMagement;

public class RegistrationService : IRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCustomerDtOs> _customerValidator;
    private readonly IValidator<CreateSellerDtOs> _sellerValidator;
    private readonly IPasswordService _passwordService;
    private readonly IFileStorageService _fileStorageService;
    private readonly IEmailService _emailService;

    public RegistrationService(
        IUnitOfWork unitOfWork,
        IValidator<CreateCustomerDtOs> customerValidator,
        IValidator<CreateSellerDtOs> sellerValidator,
        IPasswordService passwordService,
        IFileStorageService fileStorageService,
        IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _customerValidator = customerValidator;
        _sellerValidator = sellerValidator;
        _passwordService = passwordService;
        _fileStorageService = fileStorageService;
        _emailService = emailService;
    }

    public async Task<Result<CreateUserResponse>> CreateCustomer(CreateCustomerDtOs dto, CancellationToken ct)
    {
        var valResult = await _customerValidator.ValidateAsync(dto, ct);
        if (!valResult.IsValid) return ValidationError(valResult);

        return await ExecuteRegistration(dto, UserType.Customer, ct, async (userId) => 
        {
            await _unitOfWork.Customer.AddAsync(new CustomerDataModel 
            { 
                CustomrID = userId, 
                Address = dto.Address, 
                CreateAt = DateTime.UtcNow 
            }, ct);
        });
    }

    public async Task<Result<CreateUserResponse>> CreateSeller(CreateSellerDtOs dto, CancellationToken ct)
    {
        var valResult = await _sellerValidator.ValidateAsync(dto, ct);
        if (!valResult.IsValid) return ValidationError(valResult);

        return await ExecuteRegistration(dto, UserType.Seller, ct, async (userId) => 
        {
            var sellerDoc = await UploadFile(dto.VerfiedSellerDocument);
            var shopDoc = await UploadFile(dto.VerfiedShopDocument);
            var shopPhoto = await UploadFile(dto.ShopPhoto);

            await _unitOfWork.Seller.AddAsync(new SellerDataModel 
            { 
                SellerId = userId,
                ShopName = dto.ShopName,
                address = dto.Address,
                verfiedSellerDocument = sellerDoc,
                verfiedShopDocument = shopDoc,
                ShopPhoto = shopPhoto,
                isVerifiedByAdmin = false,
                isVerfiedSellerDocumentBeenViewed = false,
                isVerfiedShopDocumentBeenViewed = false,
                CreateAt = DateTime.UtcNow
            }, ct);
        });
    }

    private async Task<Result<CreateUserResponse>> ExecuteRegistration(dynamic dto, UserType role, CancellationToken ct, Func<Guid, Task> addSpecificRoleData)
    {
        var duplicateError = await CheckForDuplicateUser(dto.Email, dto.PhoneNumber, dto.UserName, ct);
        if (duplicateError != null) return Result<CreateUserResponse>.Failure(duplicateError);

        var profilePhotoUrl = await UploadFile(dto.ProfilePhoto);
        var userId = Guid.CreateVersion7();
        var otp = OTP.Generate();

        var newUser = CreateBaseUser(userId, dto, _passwordService.PasswordHash(dto.Password), profilePhotoUrl, role);
        var userOtp = CreateOtpModel(userId, otp.Value);

        // 3. Persistence
        await _unitOfWork.Users.AddAsync(newUser, ct);
        await _unitOfWork.UserOTp.AddAsync(userOtp, ct);
        
        await addSpecificRoleData(userId);

        await _unitOfWork.SaveChangesAsync(ct);

        // 4. Notifications
        await _emailService.SendOtpEmailAsync(dto.Email, otp.Value, OtpType.registration);

        return Result<CreateUserResponse>.Success(new CreateUserResponse("Registration successful. Check your email."));
    }

    private async Task<Error?> CheckForDuplicateUser(string email, string phone, string username, CancellationToken ct)
    {
        if (await _unitOfWork.Users.GetUserByEmail(email, ct) != null) return Error.Validation(
            "User.EmailUsed", "Email already in use");
        if (await _unitOfWork.Users.GetUserByPhoneNumber(phone, ct) != null) return Error.Validation(
            "User.PhoneUsed", "Phone already in use");
        if (await _unitOfWork.Users.GetUserByUserName(username, ct) != null) return Error.Validation(
            "User.UsernameUsed", "Username already in use");
        return null;
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

    private UserDataModel CreateBaseUser(Guid id, dynamic dto, string hash, string? photo, UserType role) => new()
    {
        id = id, FirstName = dto.FirstName, LastName = dto.LastName, UserName = dto.UserName,
        Email = dto.Email, password = hash, profilePhoto = photo, Role = role,DateOfBirth = dto.DateOfBirth,
        IsEmailConfirmed = false,
        AccountStatus = AccountStatus.Inactive, CreateAt = DateTime.UtcNow, phoneNumber = dto.PhoneNumber
    };

    private UserOtpDataModel CreateOtpModel(Guid userId, string code) => new()
    {
        ID = Guid.CreateVersion7(), userID = userId, Code = code, OTPtype = OtpType.registration,
        ExpiresAt = DateTime.UtcNow.AddMinutes(10), GeneratedAt = DateTime.UtcNow
    };

    private Result<CreateUserResponse> ValidationError(FluentValidation.Results.ValidationResult result) 
        => Result<CreateUserResponse>.Failure(Error.Validation("User.Validation", result.Errors.First().ErrorMessage));
}
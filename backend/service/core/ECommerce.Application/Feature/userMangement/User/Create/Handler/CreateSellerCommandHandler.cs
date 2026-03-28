using Common.DTOs.IAC.User;
using Common.Enum;
using Common.Impl.Result;
using Common.Reposotries;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.userMangement.ApplicationError;
using ECommerce.Application.Feature.userMangement.User.Create.Command;
using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Feature.userMangement.User.Create.Handler
{
    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommand, Result<CreateUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailGatway;
        private readonly IPasswordService _passwordService;
       // private readonly INotificationGateway _notificationGatway;
        private readonly IValidator<CreateSellerCommand> _validator;

        public CreateSellerCommandHandler(
            IUnitOfWork unitOfWork,
            IEmailService emailGatway,
            IPasswordService passwordService,
          //  INotificationGateway notificationGatway,
            IValidator<CreateSellerCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _emailGatway = emailGatway;
            _passwordService = passwordService;
           // _notificationGatway = notificationGatway;
            _validator = validator;
        }

        public async Task<Result<CreateUserResponse>> Handle(CreateSellerCommand command, CancellationToken cancellationToken)
        {
            // 1. Fluent Validation
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<CreateUserResponse>.Failure(
                    Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
            }

            // 2. Create Value Objects
            var firstNameVo = Name.FromStrict(command.firstName);
            var lastNameVo = Name.FromStrict(command.lastName);
            var userNameVo = Name.From(command.userName);
            var dateOfBirthVo = DateOfBirth.From(command.dateOfBirth);
            var emailVo = Email.From(command.email);
            var phoneNumberVo = PhoneNumber.From(command.phoneNumber);
            var passwordVo = Password.From(command.password);
            var addressResult = Address.Create(command.address); // تأكد من معالجة Address كـ Result

            var results = new dynamic[]
            {
                emailVo, phoneNumberVo, dateOfBirthVo, firstNameVo, lastNameVo, userNameVo, passwordVo, addressResult
            };

            var firstFailure = results.FirstOrDefault(r => r.IsError);
            if (firstFailure is not null)
            {
                return Result<CreateUserResponse>.Failure(firstFailure.Errors);
            }

            try
            {

                var existingEmail = await _unitOfWork.Users.GetUserByEmail(emailVo.Value.Value, cancellationToken);

                if (existingEmail is not null)
                    return Result<CreateUserResponse>.Failure(EmailAppError.Unique);


                var existingPhone = await _unitOfWork.Users.GetUserByPhoneNumber(phoneNumberVo.Value.Value, cancellationToken);

                if (existingPhone is not null)
                    return Result<CreateUserResponse>.Failure(PhoneAppError.Unique);

                var existingUser = await _unitOfWork.Users.GetUserByUserName(userNameVo.Value.Value, cancellationToken);

                if (existingUser is not null)
                    return Result<CreateUserResponse>.Failure(UserNameAppError.Unique);

                // 4. Identity & OTP Generation
                var id = Guid.CreateVersion7();
                var generatedOtp = OTP.Generate();

                var hashedPassword = _passwordService.PasswordHash(passwordVo.Value.Value);
                var hashedPassVo = Password.From(hashedPassword);

                // 5. Entity Creation
                var newUser = UserEntity.Create(
                    id,
                    firstNameVo.Value,
                    lastNameVo.Value,
                    userNameVo.Value,
                    dateOfBirthVo.Value,
                    emailVo.Value,
                    phoneNumberVo.Value,
                    hashedPassVo.Value,
                    UserType.Seller
                );

                newUser.SetRegisterOTP(generatedOtp.Value);

                var newSeller = SellerEntity.Create(
                    id,
                    command.shopName,
                    command.shopPhoto,
                    addressResult.Value, // استخدام الـ VO المصحح
                    false, // isVerified
                    command.verfiedShopDocument,
                    command.verfiedSellerDocument,
                    false,
                    false
                );

                // 6. Persistence
                await _unitOfWork.Users.AddAsync(newUser, cancellationToken);
                await _unitOfWork.Seller.AddAsync(newSeller.Value, cancellationToken);

                if (newUser.RegisterOTP != null)
                    await _unitOfWork.UserOTp.AddAsync(newUser.RegisterOTP, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // 7. Communication
                await _emailGatway.SendOtpEmailAsync(emailVo.Value.Value, generatedOtp.Value, OtpType.registration);
               // await _notificationGatway.SendToRoleAsync(UserRole.Admin, "Seller Verification", "New Seller registered and needs document review.");

                return Result<CreateUserResponse>.Success(new CreateUserResponse("Seller registered successfully. Please verify your email with the OTP sent."));
            }
            catch (Exception ex)
            {
                return Result<CreateUserResponse>.Failure(Error.Failure("Server.Exception", ex.Message));
            }
        }
    }
}
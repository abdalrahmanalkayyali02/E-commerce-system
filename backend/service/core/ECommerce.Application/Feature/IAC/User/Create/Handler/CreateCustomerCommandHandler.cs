using Common.DTOs.IAC.Response;
using Common.Enum;
using Common.Reposotries;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.Specfication;
using ECommerce.Domain.modules.IAC.ValueObject;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Feature.IAC.User.Create.Handler;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateUserResponse>
{
    private readonly IEmailService _emailGatway;
    private readonly IPasswordService _passwordService;
    private readonly IValidator<CreateCustomerCommand> _validater;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(
        IEmailService emailGatway,
        IPasswordService passwordService,
        IValidator<CreateCustomerCommand> validater,
        IUnitOfWork unitOfWork)
    {
        _emailGatway = emailGatway;
        _passwordService = passwordService;
        _validater = validater;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateUserResponse> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        // 1. التحقق الأولي من المدخلات (FluentValidation)
        var validationResult = await _validater.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new CreateUserResponse(validationResult.Errors.First().ErrorMessage);
        }

        try
        {
            var emailVo = Email.From(command.email);
            var phoneVo = PhoneNumber.From(command.phoneNumber);
            var dobVo = DateOfBirth.From(command.dateOfBirth);
            var firstNameVo = Name.FromStrict(command.firstName);
            var lastNameVo = Name.FromStrict(command.lastName);
            var userNameVo = Name.From(command.userName);


            // email specfication 
            var specEmail = new UserByEmailSpecification(emailVo.Value);
            var existingUserByEmail = await _unitOfWork.Users.GetEntityWithSpec(specEmail, cancellationToken);
            if (existingUserByEmail != null)
            {
                throw new Exception("Registration failed: Email already in use.");
            }


            // phone number specfication 
            var specPhone = new UserByPhoneNumberSpecfication(phoneVo.Value);
            var existingUserByPhone = await _unitOfWork.Users.GetEntityWithSpec(specPhone, cancellationToken);
            if (existingUserByPhone != null)
            {
                throw new Exception("Registration failed: Phone number already in use.");
            }


            // user name specfication 
            var specUserName = new UserByUserNameSpecfication(userNameVo.Value);
            var existingUserByUserame = await _unitOfWork.Users.GetEntityWithSpec(specUserName, cancellationToken);

            if (existingUserByUserame != null)
            {
                throw new Exception("Registeration failed : User name already use");
            }

            var userId = Guid.CreateVersion7();
            var otpCode = OTP.Generate();
            var hashedPassword = _passwordService.PasswordHash(command.password);

            var newUser = UserEntity.Create(
                userId,
                firstNameVo,
                lastNameVo,
                userNameVo,
                dobVo,
                emailVo,
                phoneVo,
                Password.From(hashedPassword),
                UserRole.Customer
            );

            newUser.SetRegisterOTP(otpCode.Value);

            var newCustomer = CustomerEntity.Create(userId, command.address);

            await _unitOfWork.Users.AddAsync(newUser, cancellationToken);
            await _unitOfWork.Customer.AddAsync(newCustomer, cancellationToken);
            await _unitOfWork.UserOTp.AddAsync(newUser.RegisterOTP!, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _emailGatway.SendOtpEmailAsync(emailVo.Value, otpCode.Value, EmailOtpType.Registration);

            return new CreateUserResponse("Customer registered successfully. Please verify your email with the OTP sent.");
        }
        catch (ArgumentException ex)
        {
            // مسك أخطاء الـ Value Objects في حال فشل الـ Parsing
            return new CreateUserResponse($"Invalid Data: {ex.Message}");
        }
        catch (Exception ex)
        {
            // مسك أي أخطاء غير متوقعة أو مشاكل الـ Database المباشرة
            throw new Exception($"An error occurred during registration: {ex.Message}");
        }
    }
}
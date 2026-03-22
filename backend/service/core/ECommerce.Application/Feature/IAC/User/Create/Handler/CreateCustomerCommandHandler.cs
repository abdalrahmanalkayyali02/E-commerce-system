using Common.DTOs.IAC.Response;
using Common.Enum;
using Common.Impl.Result;
using Common.Reposotries;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.IAC.ApplicationError;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.Specfication;
using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Domain.Modules.IAC.Entity;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Feature.IAC.User.Create.Handler;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CreateUserResponse>>
{
    private readonly IEmailService _emailGateway;
    private readonly IPasswordService _passwordService;
    private readonly IValidator<CreateCustomerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(
        IEmailService emailGateway,
        IPasswordService passwordService,
        IValidator<CreateCustomerCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _emailGateway = emailGateway;
        _passwordService = passwordService;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        // 1. Fluent Validation (Command Level)
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<CreateUserResponse>.Failure(
                Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
        }

        // 2. Create Value Objects
        var emailResult = Email.From(command.email);
        var phoneResult = PhoneNumber.From(command.phoneNumber);
        var dobResult = DateOfBirth.From(command.dateOfBirth);
        var fNameResult = Name.FromStrict(command.firstName);
        var lNameResult = Name.FromStrict(command.lastName);
        var uNameResult = Name.From(command.userName);
        var passResult = Password.From(command.password);

        var results = new dynamic[]
        {
            emailResult, phoneResult, dobResult, fNameResult, lNameResult, uNameResult, passResult
        };

        var firstFailure = results.FirstOrDefault(r => r.IsError);

        if (firstFailure is not null)
        {
            return Result<CreateUserResponse>.Failure(firstFailure.Errors);
        }

        try
        {
            var specEmail = new UserByEmailSpecification(emailResult.Value.Value);
            var existingEmail = await _unitOfWork.Users.GetEntityWithSpec(specEmail, cancellationToken);
            if (existingEmail is not null)
                return Result<CreateUserResponse>.Failure(EmailAppError.Unique);

            var specPhone = new UserByPhoneNumberSpecfication(phoneResult.Value.Value);
            var existingPhone = await _unitOfWork.Users.GetEntityWithSpec(specPhone, cancellationToken);
            if (existingPhone is not null)
                return Result<CreateUserResponse>.Failure(PhoneAppError.Unique);

            var specUser = new UserByUserNameSpecfication(uNameResult.Value.Value);
            var existingUser = await _unitOfWork.Users.GetEntityWithSpec(specUser, cancellationToken);
            if (existingUser is not null)
                return Result<CreateUserResponse>.Failure(UserNameAppError.Unique);

            var userId = Guid.CreateVersion7();
            var otpCode = OTP.Generate();

            var hashedString = _passwordService.PasswordHash(passResult.Value.Value);
            var hashedPasswordVo = Password.From(hashedString);

            var newUser = UserEntity.Create(
                userId,
                fNameResult.Value,
                lNameResult.Value,
                uNameResult.Value,
                dobResult.Value,
                emailResult.Value,
                phoneResult.Value,
                hashedPasswordVo.Value,
                UserRole.Customer
            );

            newUser.SetRegisterOTP(otpCode.Value);
            var newCustomer = CustomerEntity.Create(userId, command.address);

            await _unitOfWork.Users.AddAsync(newUser, cancellationToken);
            await _unitOfWork.Customer.AddAsync(newCustomer, cancellationToken);

            if (newUser.RegisterOTP is not null)
            {
                await _unitOfWork.UserOTp.AddAsync(newUser.RegisterOTP, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 7. Communication
            await _emailGateway.SendOtpEmailAsync(emailResult.Value.Value, otpCode.Value, EmailOtpType.Registration);

            return Result<CreateUserResponse>.Success(
                new CreateUserResponse("Registration successful. Please check your email for the OTP."));
        }
        catch (Exception ex)
        {
            return Result<CreateUserResponse>.Failure(
                Error.Failure("Server.Exception", ex.Message));
        }
    }
}
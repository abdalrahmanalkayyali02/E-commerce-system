using Common.DTOs.IAC.Response;
using Common.Enum;
using Common.Reposotries;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Domain.modules.IAC.Entity;
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
        var validationResult = await _validater.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new CreateUserResponse(validationResult.Errors.First().ErrorMessage);
        }

        try
        {
            var existingUser = await _unitOfWork.Repo<UserEntity>()
                .FindAsync(x => x.Email.Value == command.email);

            if (existingUser != null)
            {
                return new CreateUserResponse("Registration failed: Email already in use.");
            }

            var id = Guid.CreateVersion7();
            var otp = OTP.Generate();
            var hashedPassword = _passwordService.PasswordHash(command.password);

            var newUser = UserEntity.Create(
                id,
                Name.FromStrict(command.firstName),
                Name.FromStrict(command.lastName),
                Name.From(command.userName),
                DateOfBirth.From(command.dateOfBirth),
                Email.From(command.email),
                PhoneNumber.From(command.phoneNumber),
                Password.From(hashedPassword),
                UserRole.Customer
            );

            newUser.SetRegisterOTP(otp.Value);

            var newCustomer = CustomerEntity.Create(id, command.address);

            await _unitOfWork.Repo<UserEntity>().AddAsync(newUser, cancellationToken);
            await _unitOfWork.Repo<CustomerEntity>().AddAsync(newCustomer, cancellationToken);
            await _unitOfWork.Repo<UserOTPEntity>().AddAsync(newUser.RegisterOTP!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _emailGatway.SendOtpEmailAsync(command.email, otp.Value, EmailOtpType.Registration);
         
            return new CreateUserResponse("Customer registered successfully. Please verify your email with the OTP sent.");
        }
        catch (ArgumentException ex)
        {
            return new CreateUserResponse($"Invalid Data: {ex.Message}");
        }
      
    }
}
using Common.Abstraction.Reposotries;
using ECommerce.Application.DTO.IAC.User.Response;
using ECommerce.Application.Feature.IAC.Commands.CreateUser.Command;
using FluentValidation;
using IAC.Application.Abstraction;
using IAC.Domain.AggregateRoot;
using IAC.Domain.Enum;
using IAC.Domain.Repository.Read;
using IAC.Domain.Repository.Write;
using IAC.Domain.Value_Object;
using MediatR;

namespace ECommerce.Application.Feature.IAC.Commands.CreateUser.Handler;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateUserResponse>
{
    private readonly IUserReadRepository _userReadRepo;
    private readonly IUserWriteRepository _userWriteRepo;
    private readonly ICustomerWriteRepository _customerWriteRepo;
    private readonly IUserOTpWriteRepository _userOTpWriteRepo;
    private readonly IEmailGatway _emailGatway;
    private readonly IPasswordService _passwordService;
    private readonly IValidator<CreateCustomerCommand> _validater;
    private readonly IUnitOfWork _unitOfWork; 

    public CreateCustomerCommandHandler(
        IUserReadRepository userReadRepo,
        IUserWriteRepository userWriteRepo,
        ICustomerWriteRepository customerWriteRepo,
        IUserOTpWriteRepository _userOTpWriteRepo,
        IEmailGatway emailGatway,
        IPasswordService passwordService,
        IValidator<CreateCustomerCommand> validater,
        IUnitOfWork unitOfWork)
     
    {
        _userReadRepo = userReadRepo;
        _userWriteRepo = userWriteRepo;
        _customerWriteRepo = customerWriteRepo;
        _customerWriteRepo = customerWriteRepo;
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
            var existingUser = await _userReadRepo.GetUserByEmail(command.email, cancellationToken);
            if (existingUser != null)
            {
                return new CreateUserResponse("Registration failed: Email already in use.");
            }

            var id = Guid.CreateVersion7();
            var otp = OTP.Generate();
            var hashedPassword = _passwordService.PasswordHash(command.password);

            var newUser = UserAggregate.Create(
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

            var newCustomer = CustomerAggregate.Create(id, command.address);

            // 6. Persistence
            await _userWriteRepo.AddAsync(newUser, cancellationToken);
            await _customerWriteRepo.AddAsync(newCustomer, cancellationToken);
            await _userOTpWriteRepo.AddAsync(newUser.RegisterOTP!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            await _emailGatway.SendOtpEmailAsync(
                command.email,
                otp.Value,
                EmailOtpType.Registration
            );

            return new CreateUserResponse("Customer registered successfully. Please verify your email with the OTP sent.");
        }
        catch (ArgumentException ex)
        {
            return new CreateUserResponse($"Invalid Data: {ex.Message}");
        }
      
    }
}
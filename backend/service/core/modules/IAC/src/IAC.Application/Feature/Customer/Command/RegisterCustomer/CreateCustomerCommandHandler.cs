using IAC.Application.Abstraction;
using IAC.Domain.AggregateRoot;
using IAC.Domain.Enum;
using IAC.Domain.Repository.Read;
using IAC.Domain.Repository.Write;
using IAC.Domain.Value_Object;
using MediatR;

namespace IAC.Application.Feature.Customer.Command.RegisterCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, RegisterCustomerResult>
    {
        private readonly IPasswordService _passwordService;
        //private readonly IEmailGatway _emailGatway;
        private readonly IUserReadRepository _readUserRepo;
        private readonly IUserWriteRepository _userWriteRepo;
        private readonly ICustomerWriteRepository _customerWriteRepo;
        // private readonly IUnitOfWork _unitOfWork; // will be important in the future for transaction management


        public CreateCustomerCommandHandler(
            IPasswordService passwordService,
          //  IEmailGatway emailGatway,
            IUserReadRepository userRepo,
            IUserWriteRepository userWriteRepo,
            ICustomerWriteRepository customerWriteRepo)
        {
            _passwordService = passwordService;
           // _emailGatway = emailGatway;
            _readUserRepo = userRepo;
            _userWriteRepo = userWriteRepo;
            _customerWriteRepo = customerWriteRepo;
        }

        public async Task<RegisterCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _readUserRepo.GetUserByEmail(request.Email);
            if (userExist is not null)
            {
                throw new InvalidOperationException($"Email {request.Email} is already in use.");
            }

            var userId = Guid.CreateVersion7();
            var hashedPassword = _passwordService.PasswordHash(request.Password);

            var user = UserAggregate.Create(
                userId,
                Name.FromStrict(request.FirstName),
                Name.FromStrict(request.LastName),
                Name.From(request.UserName),
                DateOfBirth.From(request.DateOfBirth),
                Email.From(request.Email),
                PhoneNumber.From(request.PhoneNumber),
                Password.From(hashedPassword),
                UserRole.Customer
            );

            var customer = CustomerAggregate.Create(
                userId,
                request.Address
            );

            var otp = OTP.Generate();
            user.SetRegisterOTP(otp.Value);

            await _userWriteRepo.AddAsync(user);
            await _customerWriteRepo.AddAsync(customer);

            // await _unitOfWork.SaveChangesAsync(cancellationToken); // الخطوة الأهم مستقبلاً

            // 6. إرسال الإيميل (يفضل يكون بعد الـ Save أو عن طريق Domain Events)
            // await _emailGatway.SendOtpEmailAsync(user.Email.Value, otp.Value, EmailOtpType.Registration);

            return new RegisterCustomerResult("The customer registered successfully", userId);
        }
    }
}
//using Common.Enum;
//using Common.Reposotries;
//using ECommerce.Application.Abstraction.IExternalService;
//using ECommerce.Application.DTO.IAC.User.Response;
//using ECommerce.Application.Feature.IAC.Commands.CreateUser.Command;
//using ECommerce.Domain.modules.IAC.Entity;
//using ECommerce.Domain.modules.IAC.Repositories.Read;
//using ECommerce.Domain.modules.IAC.Repositories.Write;
//using ECommerce.Domain.modules.IAC.ValueObject;
//using FluentValidation;
//using MediatR;


//namespace ECommerce.Application.Feature.IAC.Commands.CreateUser.Handler
//{
//    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommand,CreateUserResponse>
//    {
//        private readonly IUserReadRepository _userReadRepository;
//        private readonly IUserWriteRepository _userWriteRepository;
//        private readonly ISellerWriteRepository _sellerWriteRepository;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IEmailService _emailGatway;
//        private readonly IPasswordService _passwordService;
//        private readonly INotificationGateway _notificationGatway;
//        private readonly IValidator<CreateSellerCommand> _validator;
        


//        public CreateSellerCommandHandler(
//            IUserReadRepository userReadRepo, IUserWriteRepository userWriteRepo,ISellerWriteRepository sellerWriteRepo,
//            IUnitOfWork unitOfWork,IEmailService emailGatway, IPasswordService passwordService,
//            INotificationGateway notificationGatway, IValidator<CreateSellerCommand> _validator)
//        {
//            _userReadRepository = userReadRepo;
//            _userWriteRepository = userWriteRepo;
//            _sellerWriteRepository = sellerWriteRepo;
//            _unitOfWork = unitOfWork;
//            _emailGatway = emailGatway;
//            _passwordService = passwordService;
//            _notificationGatway = notificationGatway;
//            this._validator = _validator;
//        }

//        public async Task <CreateUserResponse> Handle (CreateSellerCommand command, CancellationToken cancellationToken)
//        {
//            var validateResult = await _validator.ValidateAsync(command);
            
//            if (!validateResult.IsValid)
//            {
//                return new CreateUserResponse(validateResult.Errors.First().ErrorMessage); 
//            }

//            try
//            {
//                var id = Guid.CreateVersion7();
//                var hashedPassword = _passwordService.PasswordHash(command.password);
//                var otp = OTP.Generate();


//                var newUser = UserEntity.Create(
//                    id,
//                    Name.FromStrict(command.firstName),
//                    Name.FromStrict(command.lastName),
//                    Name.From(command.userName),
//                    DateOfBirth.From(command.dateOfBirth),
//                    Email.From(command.email),
//                    PhoneNumber.From(command.phoneNumber),
//                    Password.From(hashedPassword),
//                    UserRole.Customer
//                );

//                newUser.SetRegisterOTP(otp.Value);

//                var newSeller = SellerEntity.Create(
//                    id,
//                    command.shopName,
//                    command.shopPhoto,
//                    Address.Create(command.address),
//                    false,
//                    command.verfiedShopDocument,
//                    command.verfiedSellerDocument)
//                    ;

//                await _userWriteRepository.AddAsync(newUser, cancellationToken);
//                await _sellerWriteRepository.AddAsync(newSeller,cancellationToken);
//                await _unitOfWork.SaveChangesAsync(cancellationToken);

//                await _emailGatway.SendOtpEmailAsync(command.email, otp.Value, EmailOtpType.Registration);
//                await _notificationGatway.SendToRoleAsync(UserRole.Admin, "Seller Verfication", "Verfied Sellerand shop Document");

//                return new CreateUserResponse("Customer registered successfully. Please verify your email with the OTP sent.");

//            } catch (Exception ex)
//            {
//                return new CreateUserResponse($"Invalid Data: {ex.Message}");
//            }

//        }
            
//    }
//}

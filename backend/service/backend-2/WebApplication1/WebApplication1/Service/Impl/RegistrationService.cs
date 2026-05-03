namespace WebApplication1.Service.Impl;

using FluentValidation;
using MediatR;
using WebApplication1.DTOs;
using WebApplication1.Service.IExternalService;
using WebApplication1.Service.Interface;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;
using WEbApplication1.Service.IExternalService;

public class RegistrationService : IRegistrationService
{
    // for create the customer user 
    private readonly IMediator _mediator;
    private readonly IValidator<CreateCustomerDTOs> CreateCustomerDTOsValidator; 
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    private readonly IFileStorageService _fileStorageService;


    public RegistrationService
    (
        IMediator mediator,
         IValidator<CreateCustomerDTOs> createCustomerDTOsValidator,
         IEmailService emailService,
         IPasswordService passwordService,
         IFileStorageService fileStorageService
)
    {
        _mediator = mediator;
        CreateCustomerDTOsValidator = createCustomerDTOsValidator;
        _emailService = emailService;
        _passwordService = passwordService;
        _fileStorageService = fileStorageService;
    }
    
    public Task<Result<CreateUserResponse>> CreateCustomer(CreateCustomerDTOs createCustomerDTOs)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CreateUserResponse>> CreateSeller(CreateSellerDTOs createSellerDTOs)
    {
        throw new NotImplementedException();
    }
 }



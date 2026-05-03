using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;

namespace WebApplication1.Service.Impl;

using FluentValidation;
using MediatR;
using WebApplication1.DTOs;
using WebApplication1.Service.IExternalService;
using WebApplication1.Service.Interface;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

public class RegistrationService : IRegistrationService
{
    // for create the customer user 
    private readonly IMediator _mediator;
    private readonly IValidator<CreateCustomerDtOs> _validator; 
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;


    public RegistrationService
    (
        IMediator mediator,
         IValidator<CreateCustomerDtOs> createCustomerDTOsValidator,
         IEmailService emailService,
         IPasswordService passwordService,
         IFileStorageService fileStorageService,
        IUnitOfWork unitOfWork
)
    {
        _mediator = mediator;
        _validator = createCustomerDTOsValidator;
        _emailService = emailService;
        _passwordService = passwordService;
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
    }
    
    public Task<Result<CreateUserResponse>> CreateCustomer(CreateCustomerDtOs createCustomerDTOs)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CreateUserResponse>> CreateSeller(CreateSellerDtOs createSellerDTOs)
    {
        throw new NotImplementedException();
    }
 }



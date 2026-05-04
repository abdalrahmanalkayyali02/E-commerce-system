using FluentValidation;
using WebApplication1.DTOs;
using WebApplication1.Repository.Impl;
using WebApplication1.Repository.Interface;
using WebApplication1.Service.IExternalService.Abstraction;
using WebApplication1.Service.IExternalService.Impl;
using WebApplication1.Validation;

namespace WebApplication1.DI;
 public static class InfrastructureServiceRegistration
 {
     public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
     {
         services.AddInternalServices(configuration);
         services.AddExternalServices(configuration);
         services.AddRegisterationService(configuration);

         return services;
     }

     public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
     {
         services.AddScoped<IEmailService, MailTripEmailService>();
         services.AddScoped<IFileStorageService,ClaudunaryFileStorgeService>();

         return services;
     }

     public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
     {
         services.AddScoped<IPasswordService, BCryptPasswordService>();
        // services.AddScoped<IJWTService, JWTService>();

         return services;
     }

     public static IServiceCollection AddRegisterationService(this IServiceCollection services, IConfiguration configuration)
     {
         // unit of work
         services.AddScoped<IUnitOfWork, UnitOfWork>();

         // repository
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<ICustomerRepository, CustomerRepository>();
         services.AddScoped<ISellerRepository, SellerRepository>();
         services.AddScoped<IUserOTpRepository, UserOtpRepository>();

         // services.AddScoped<ProfileStrategyContext>();
         // services.AddScoped<IProfileStrategy, UserProfileStrategy>();
         // services.AddScoped<IProfileStrategy, CustomerProfileStrategy>();
         // services.AddScoped<IProfileStrategy, SellerProfileStrategy>();

         
         // Validator Registration
         services.AddScoped<IValidator<CreateCustomerDtOs>, CreateCustomerDtOsValidator>();
         services.AddScoped<IValidator<CreateSellerDtOs>, CreateSellerDTOsValidatior>();
         
         // services.AddScoped<IValidator<LoginUserCommand>, LoginValidater>();
         // services.AddScoped<IValidator<updateForgetPasswordCommand>, UpdateForgetPasswordCommandValidater>();
         // services.AddScoped<IValidator<userProfileCommand>,userProfileCommandValidater>();
         // services.AddScoped<IValidator<customerProfileCommand>, customerProfileCommandValidater>();
         // services.AddScoped<IValidator<sellerProfileCommand>, sellerProfileCommandValidater>();
         // services.AddScoped<IValidator<GetAllUsersByFilterQuery>, GetAllUsersByFilterQueryValidator>();
         // services.AddScoped<IValidator<UpdateShopDetailCommand>, UpdateShopDetailCommandValidator>();



         return services;
     }
 }
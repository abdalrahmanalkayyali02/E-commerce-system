using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.Catalog.Product.Create.Command;
using ECommerce.Application.Feature.Catalog.Product.Create.Validator;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Application.Feature.IAC.User.Create.Validater;
using ECommerce.Domain.modules.Catalog.Repository;
using ECommerce.Domain.modules.IAC.Repositories;
using ECommerce.Infrastructure.ExternalService;
using ECommerce.Infrastructure.Persistence.Repository;
using ECommerce.Infrastructure.Persistence.Repository.Catalog;
using ECommerce.Infrastructure.Persistence.Repository.IAC;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.DIC
{
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
            services.AddScoped<IFileStorgeService,ClaudunaryFileStorgeService>();

            return services;
        }

        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordService, BCryptPasswordService>();

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

            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductCatogryRepository,ProductCategoryRepository>();
            services.AddScoped<IProductPhotoRepository,ProductPhotoRepository>();
            

            

            // Validator Registration
            services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();
            services.AddScoped<IValidator<CreateSellerCommand>, CreateSellerValidater>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();


            return services;
        }
    }
}
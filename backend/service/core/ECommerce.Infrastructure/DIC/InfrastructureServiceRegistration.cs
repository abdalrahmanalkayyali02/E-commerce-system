using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Query;
using ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Validator;
using ECommerce.Application.Feature.Catalog.Product.Create.Command;
using ECommerce.Application.Feature.Catalog.Product.Create.Validater;
using ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Query;
using ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Validator;
using ECommerce.Application.Feature.userMangement.User.Create.Command;
using ECommerce.Application.Feature.userMangement.User.Create.Validater;
using ECommerce.Application.Feature.userMangement.User.Login.Command;
using ECommerce.Application.Feature.userMangement.User.Login.Validater;
using ECommerce.Application.Feature.userMangement.User.Profile;
using ECommerce.Application.Feature.userMangement.User.Profile.Strategy;
using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Validater;
using ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Command;
using ECommerce.Domain.modules.Catalog.Repository;
using ECommerce.Domain.modules.UserMangement.Repositories;
using ECommerce.Infrastructure.ExternalService;
using ECommerce.Infrastructure.Persistence.Repository;
using ECommerce.Infrastructure.Persistence.Repository.Catalog;
using ECommerce.Infrastructure.Persistence.Repository.Notification;
using ECommerce.Infrastructure.Persistence.Repository.UserMangement;
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
            services.AddScoped<IJWTService, JWTService>();

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

            services.AddScoped<ProfileStrategyContext>();
            services.AddScoped<IProfileStrategy, UserProfileStrategy>();
            services.AddScoped<IProfileStrategy, CustomerProfileStrategy>();
            services.AddScoped<IProfileStrategy, SellerProfileStrategy>();




            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductCatogryRepository,ProductCategoryRepository>();
            services.AddScoped<IProductPhotoRepository,ProductPhotoRepository>();


            services.AddScoped<INotificationRepository, NotificationRepository>();




            // Validator Registration
            services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();
            services.AddScoped<IValidator<CreateSellerCommand>, CreateSellerValidater>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddScoped<IValidator<LoginUserCommand>, LoginValidater>();
            services.AddScoped<IValidator<updateForgetPasswordCommand>, UpdateForgetPasswordCommandValidater>();
            services.AddScoped<IValidator<userProfileCommand>,userProfileCommandValidater>();
            services.AddScoped<IValidator<customerProfileCommand>, customerProfileCommandValidater>();
            services.AddScoped<IValidator<sellerProfileCommand>, sellerProfileCommandValidater>();
            services.AddScoped<IValidator<GetAllNotificationQuery>,GetAllNotificationQueryValidator>();
            services.AddScoped<IValidator<GetAllUsersByFilterQuery>, GetAllUsersByFilterQueryValidator>();



            return services;
        }
    }
}
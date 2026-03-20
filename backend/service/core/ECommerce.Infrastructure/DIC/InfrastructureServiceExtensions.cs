using Common.Reposotries;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Infrastructure.ExternalService;
using ECommerce.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace ECommerce.Infrastructure.DIC
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, MailTripEmailService>();
           // services.AddScoped<INotificationGateway, FirebaseCloudMessagingNotificationgatway>();

            // add other external services like payment gateway, sms gateway, etc.



            return services;
        }

        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IPasswordService, BCryptPasswordService>();
          //  services.AddScoped<IJWTService, IJWTService>();
            // add other internal services like caching, logging, etc.

            return services;

        }
       public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }


          

        


        

    }
}

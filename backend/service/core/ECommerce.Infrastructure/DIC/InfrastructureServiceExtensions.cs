using ECommerce.Application.Abstraction;
using ECommerce.Infrastructure.Service;
using IAC.Application.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailGatway, MailTripEmailGatway>();
            services.AddScoped<INotificationGateway, FirebaseCloudMessagingNotificationgatway>();

            // add other external services like payment gateway, sms gateway, etc.



            return services;
        }

        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IPasswordService, BCryptPasswordService>();
            services.AddScoped<IJWTService, IJWTService>();


            // add other internal services like caching, logging, etc.

            return services;
        }

    }
}

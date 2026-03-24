using ECommerce.Application.Feature.Catalog.Product.Create.Command;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.DIC
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {

                cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateSellerCommand).Assembly);

                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);


            });


            return services;
        }
    }
}

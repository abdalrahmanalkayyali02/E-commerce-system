using ECommerce.Application.Feature.Catalog.Product.Create.Command;
using ECommerce.Application.Feature.userMangement.OtpVerification.Verified.Command;
using ECommerce.Application.Feature.userMangement.User.Create.Command;
using ECommerce.Application.Feature.userMangement.User.Login.Command;
using ECommerce.Application.Feature.userMangement.User.Profile.GetProfile.Queries;
using ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Command;
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
                cfg.RegisterServicesFromAssemblies(typeof(VerfiedOtpCommand).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(ResendOtpCommand).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(GetUserByIdQueries).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(LoginUserCommand).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(updateForgetPasswordCommand).Assembly);

                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);



            });


            return services;
        }
    }
}

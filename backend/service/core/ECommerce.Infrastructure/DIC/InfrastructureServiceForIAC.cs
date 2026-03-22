using ECommerce.Application.Abstraction;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Application.Feature.IAC.User.Create.Validater;
using ECommerce.Application.Feature.IAC.User.Login.Command;
using ECommerce.Application.Feature.IAC.User.Login.Validater;
using ECommerce.Domain.modules.IAC.Repositories;
using ECommerce.Infrastructure.Persistence.Repository;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.Infrastructure.DIC
{
    public static class InfrastructureServiceForIAC
    {
        public static void AddInfrastructureServicesForIAC(this IServiceCollection services, IConfiguration configuration)
        {
            // shared service registeration

            // Repository registeration
            services.AddScoped<IUserRepository, UserRepository> ();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IUserOTpRepository, UserOtpRepository>();

            // validater
            services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();

            services.AddScoped<IValidator<CreateSellerCommand>, CreateSellerValidater>();
            // services.AddScoped<IValidator<LoginUserCommand>, LoginValidater>();


            // verfied otp


            // resent otp 


            // update forget password


            // get profile


            // update profile 


        }
    }
}

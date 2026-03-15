using ECommerce.Application.Abstraction;
using ECommerce.Application.Feature.IAC.Commands.CreateUser.Command;
using ECommerce.Application.Feature.IAC.Commands.CreateUser.Validater;
using ECommerce.Application.Feature.IAC.Commands.Login.Command;
using ECommerce.Application.Feature.IAC.Commands.Login.Validater;
using ECommerce.Infrastructure.Persistence.Repository.Read;
using ECommerce.Infrastructure.Persistence.Repository.Write;
using ECommerce.Infrastructure.Service;
using FluentValidation;
using IAC.Application.Abstraction;
using IAC.Domain.Repository.Read;
using IAC.Domain.Repository.Write;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.Infrastructure.DIC
{
    public static  class InfrastructureServiceForIAC
    {
        public static void AddInfrastructureServicesForIAC(this IServiceCollection services, IConfiguration configuration)
        {
            // shared service registeration
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            
         
            //sppecfic target 
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddScoped<ISellerWriteRepository, SellerWriteRepository>();
            
            // validater
            services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();
            services.AddScoped<IValidator<CreateSellerCommand>, CreateSellerValidater>();
            services.AddScoped<IValidator<LoginUserCommand>, LoginValidater>();


            // verfied otp


            // resent otp 


            // update forget password


            // get profile


            // update profile 


        }
    }
}

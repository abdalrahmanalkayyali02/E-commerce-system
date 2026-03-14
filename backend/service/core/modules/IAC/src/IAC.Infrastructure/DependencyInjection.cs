using IAC.Application.Abstraction;
using IAC.Domain.Repository.Read;
using IAC.Domain.Repository.Write;
using IAC.Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositories
       // services.AddScoped<IUserWriteRepository, UserWriteRepository>();
      //  services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        //services.AddScoped<IUserReadRepository, UserReadRepository>();

        // Services
        services.AddScoped<IPasswordService, BCryptPasswordService>();
      //  services.AddScoped<IEmailGateway, SendGridEmailGateway>();



        // Database context (EF Core)
        //services.AddDbContext<ApplicationDbContext>(options =>
          //  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
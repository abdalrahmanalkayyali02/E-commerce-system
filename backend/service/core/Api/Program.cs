using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.DIC;
var builder = WebApplication.CreateBuilder(args);

// add service here 
builder.Services.AddControllers();



builder.Services.AddControllers();

//builder.Services.AddApiVersioning(option =>
//{
//    option.DefaultApiVersion = new ApiVersion(1, 0);
//    option.AssumeDefaultVersionWhenUnspecified = true;
//    option.ReportApiVersions = true;
//    option.ApiVersionReader = new MediaTypeApiVersionReader("v");
//});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructureServicesForIAC(builder.Configuration);
builder.Services.AddInternalServices(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// add controller here 
// add the middleware - pipline 

app.Run();
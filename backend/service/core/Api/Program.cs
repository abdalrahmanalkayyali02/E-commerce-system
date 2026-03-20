using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Infrastructure.DIC;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- 2. DATABASE CONFIGURATION ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsAssembly("ECommerce.Infrastructure")
    ));

builder.Services.AddInfrastructureServicesForIAC(builder.Configuration);
builder.Services.AddInternalServices(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);
builder.Services.AddSharedServices(builder.Configuration);

// --- 4. MEDIATR ---
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "E-Commerce IAC API",
        Version = "v1",
        Description = "Identity and Access Control (IAC) module for E-Commerce System"
    });

    // Optional: Enable XML comments if you have them enabled in project properties
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// --- 6. MIDDLEWARE PIPELINE ---

// Enable Swagger UI only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // Set Swagger as the default landing page
    });
}

// Ensure proper routing and security order
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using Api.Extensions;
using Api.Middleware;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddSwaggerConfig();
builder.Services.AddApplicationLayerServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCustomSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowFrontend");

app.UseMiddleware<GlobalResultMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Custom Auth Check
app.UseMiddleware<IsAuthMiddleware>();

app.MapControllers();

app.Run();
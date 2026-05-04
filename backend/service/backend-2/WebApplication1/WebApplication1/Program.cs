
using WebApplication1;
using WebApplication1.Middleware;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddSwaggerConfig();
builder.Services.AddApplicationLayerServices(builder.Configuration);


var app = builder.Build();

app.UseMiddleware<IsAuthMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCustomSwaggerUi();
}

app.UseRouting();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalResultMiddleware>();



app.MapControllers();

app.Run();
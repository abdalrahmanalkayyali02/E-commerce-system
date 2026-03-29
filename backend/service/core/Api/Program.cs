using Api.Constraints;
using Api.Middleware;
using ECommerce.Infrastructure.DIC;
using ECommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

// --- 1. INITIAL CONFIGURATION ---
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

// --- 2. DATABASE CONFIGURATION ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsAssembly("ECommerce.Infrastructure")
    ));

// --- 3. CUSTOM SERVICES ---
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("otpType", typeof(OtpTypeRouteConstraint));
});

// --- 4. AUTHENTICATION & JWT ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSetting = builder.Configuration.GetSection("JWTSettings");
    var secretKey = jwtSetting["SecretKey"];

    if (string.IsNullOrEmpty(secretKey))
    {
        throw new InvalidOperationException("JWT SecretKey is missing in configuration!");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = jwtSetting["Issuer"],
        ValidAudience = jwtSetting["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };

    // --- THIS PART SOLVES THE BLANK 401 ISSUE ---
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Skip default logic so the request reaches our custom middleware
            context.HandleResponse();
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            // Ensure 403s also reach our custom middleware
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }
    };
});

// --- 5. SWAGGER CONFIGURATION ---
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "E-Commerce API",
        Version = "v1",
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// --- 6. MIDDLEWARE PIPELINE ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// app.UseHttpsRedirection(); // Keep commented if you have SSL issues locally

app.UseRouting();

// Order is vital: IsAuthMiddleware must be AFTER UseAuthorization
app.UseMiddleware<IsAuthMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// This will now catch the 401/403 and write your Result JSON body

app.MapControllers();

app.Run();
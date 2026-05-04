using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using WebApplication1.Data;
using WebApplication1.DI;
using WebApplication1.RouteConstrain;
namespace WebApplication1;

public static class ServiceExtensions
{
    public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("DefaultConnection")
            ));
    }

    // public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    // {
    //     var jwtSetting = config.GetSection("JWTSettings");
    //     var secretKey = jwtSetting["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is missing!");
    //
    //     services.AddAuthentication(options =>
    //     {
    //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //     })
    //     .AddJwtBearer(options =>
    //     {
    //         options.TokenValidationParameters = new TokenValidationParameters
    //         {
    //             ValidateIssuer = true,
    //             ValidateAudience = true,
    //             ValidateLifetime = true,
    //             ValidateIssuerSigningKey = true,
    //             ClockSkew = TimeSpan.Zero,
    //             ValidIssuer = jwtSetting["Issuer"],
    //             ValidAudience = jwtSetting["Audience"],
    //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    //         };
    //
    //         options.Events = new JwtBearerEvents
    //         {
    //             OnChallenge = context =>
    //             {
    //                 context.HandleResponse();
    //                 context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //                 return Task.CompletedTask;
    //             },
    //             OnForbidden = context =>
    //             {
    //                 context.Response.StatusCode = StatusCodes.Status403Forbidden;
    //                 return Task.CompletedTask;
    //             }
    //         };
    //     });
    // }

    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
        });
    }

    public static void AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
    }

    public static void AddApplicationLayerServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddInfrastructureServices(config);
        services.Configure<RouteOptions>(options =>
        {
            options.ConstraintMap.Add("otpType", typeof(OtpTypeRouteConstraint));
        });
    }

    public static void UseCustomSwaggerUi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }
}
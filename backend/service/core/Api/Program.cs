using IAC.Infrastructure.Persistence.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add service here 
builder.Services.AddControllers();


var app = builder.Build();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// add controller here 
// add the middleware - pipline 

app.Run();
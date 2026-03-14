using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using IAC.Infrastructure.Persistence.Model;
using IAC.Infrastructure.Persistence.Models;

namespace IAC.Infrastructure.Persistence.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserOtpModel> Otps { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<SellerModel> Sellers { get; set; }

      
    }
}
using ECommerce.Infrastructure.Persistence.Model.IAC;
using ECommerce.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace ECommerce.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserDataModel> Users { get; set; }
        public DbSet<CustomerDataModel> Customers { get; set; }
        public DbSet<UserOtpDataModel> UserOtps { get; set; }

        public DbSet<SellerDataModel> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<UserDataModel>().HasData(SeedForIAC.GetUsers());
            //modelBuilder.Entity<CustomerDataModel>().HasData(SeedForIAC.GetCustomers());
            //modelBuilder.Entity<SellerDataModel>().HasData(SeedForIAC.GetSellers());
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            //later will fix the namee conversion issue
        }

    
    }
}
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Model;

namespace WebApplication1.Data
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
         
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            //later will fix the namee conversion issue
        }

 
    }
}

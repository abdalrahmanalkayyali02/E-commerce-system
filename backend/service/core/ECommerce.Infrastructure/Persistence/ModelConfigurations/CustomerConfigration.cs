using ECommerce.Infrastructure.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.ModelConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerDataModel>
    {
        public void Configure(EntityTypeBuilder<CustomerDataModel> builder)
        {
            // 1. Table Mapping
            builder.ToTable("Customers", "iac");

            // 2. Primary Key
            builder.HasKey(c => c.CustomrID);

            // 3. Property Constraints
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(250); // Matches your Domain logic (max 150) plus buffer



            builder.Property(c => c.CreateAt)
                .IsRequired();

            builder.Property(c => c.UpdateAt)
                .IsRequired();

            builder.HasOne<UserDataModel>()
                .WithOne()
                .HasForeignKey<CustomerDataModel>(c => c.CustomrID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
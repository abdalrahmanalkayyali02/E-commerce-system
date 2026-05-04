using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Model;

namespace WebApplication1.Data.ModelConfigration;

public class CustomerModelConfigration : IEntityTypeConfiguration<CustomerDataModel>
{
    public void Configure(EntityTypeBuilder<CustomerDataModel> builder)
    {
        // 1. Table Mapping
        builder.ToTable("Customers");

        // 2. Primary Key
        builder.HasKey(c => c.CustomrID);

        // 3. Property Constraints
        builder.Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(250);
        
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

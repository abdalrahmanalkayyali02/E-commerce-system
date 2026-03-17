using ECommerce.Infrastructure.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDataModel>
    {
        public void Configure(EntityTypeBuilder<UserDataModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.id);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.password)
                .IsRequired();

            builder.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(u => u.AccountStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasQueryFilter(u => !u.isDelete);
        }
    }
}
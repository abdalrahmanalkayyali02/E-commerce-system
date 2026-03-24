using ECommerce.Infrastructure.Persistence.Model.IAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.ModelConfigurations.IAC
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDataModel>
    {
        public void Configure(EntityTypeBuilder<UserDataModel> builder)
        {
            builder.ToTable("Users", "iac");

            builder.HasKey(u => u.id);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.HasIndex(u => u.UserName).IsUnique();

            // حل مشكلة الصيغة لـ Postgres
            builder.Property(u => u.DateOfBirth).HasColumnType("date");

            builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.IsEmailConfirmed).HasDefaultValue(false);
            builder.Property(u => u.phoneNumber).IsRequired();
            builder.HasIndex(u => u.phoneNumber).IsUnique();

            builder.Property(u => u.password).IsRequired();

            builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
            builder.Property(u => u.AccountStatus).HasConversion<string>().HasMaxLength(20);

            builder.Property(u => u.profilePhoto).IsRequired(false);

            builder.HasQueryFilter(u => !u.isDelete);

            builder.Property(u => u.CreateAt)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.Property(u => u.UpdateAt)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.Property(u => u.DeleteAt)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null
                );
        }
    }
}
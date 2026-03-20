using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Infrastructure.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.ModelConfigurations

{
    public class UserOtpConfiguration : IEntityTypeConfiguration<UserOtpDataModel>
    {
        public void Configure(EntityTypeBuilder<UserOtpDataModel> builder)
        {
            builder.ToTable("UserOTPs", "iac"); 

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Code)
                   .HasColumnName("Code")
                   .HasMaxLength(6)
                   .IsRequired(false);

            builder.Property(x => x.GeneratedAt)
                .IsRequired();

            builder.Property(x => x.ExpiresAt)
                .IsRequired();

            builder.Property(x => x.IsUsed)
                .HasDefaultValue(false);

            builder.Property(x => x.IsVerified)
                .HasDefaultValue(false);

            builder.Property(x => x.FailedAttempts)
                .HasDefaultValue(0);

            builder.HasOne<UserDataModel>()
                .WithMany() 
                .HasForeignKey(x => x.userID)
                .OnDelete(DeleteBehavior.Cascade); // for if user deleted delete all user otp 

            // for indexing 
            builder.HasIndex(x => x.userID);
            builder.HasIndex(x => new { x.userID, x.Code }).IsUnique(); 
        }
    }
}
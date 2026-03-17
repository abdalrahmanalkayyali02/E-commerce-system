using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Infrastructure.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECommerce.Domain.modules.IAC.ValueObject;

namespace ECommerce.Infrastructure.Persistence.Configurations
    
{
    public class UserOtpConfiguration : IEntityTypeConfiguration<UserOtpDataModel>
    {
        public void Configure(EntityTypeBuilder<UserOtpDataModel> builder)
        {
            builder.ToTable("UserOTPs", "iac"); 

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Code)
                .HasConversion(
                v => v.Value,            // How to save: OTP -> string
                v => OTP.From(v))    // How to load: string -> OTP)
                .HasMaxLength(6);

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
using ECommerce.Infrastructure.Persistence.Model.UserMangement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.ModelConfigurations.UserMangement
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
                   .IsRequired();

            builder.Property(x => x.GeneratedAt)
                .IsRequired();

            builder.Property(x => x.ExpiresAt)
                .IsRequired();

            builder.Property(x => x.UpdateAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); 

            builder.Property(x => x.TimeVerfied)
                .IsRequired(false);

            builder.Property(x => x.IsUsed)
                .HasDefaultValue(false);

            builder.Property(x => x.IsVerified)
                .HasDefaultValue(false);

            builder.Property(x => x.FailedAttempts)
                .HasDefaultValue(0);

            builder.Property(x => x.OTPtype)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne<UserDataModel>()
                .WithMany()
                .HasForeignKey(x => x.userID)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(x => x.userID);

            
            builder.HasIndex(x => new { x.userID, x.Code }).IsUnique();

            builder.HasIndex(x => new { x.userID, x.UpdateAt, x.IsUsed });
        }
    }
}
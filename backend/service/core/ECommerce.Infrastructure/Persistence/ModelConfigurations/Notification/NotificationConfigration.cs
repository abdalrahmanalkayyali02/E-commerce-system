using Common.Enum;
using ECommerce.Infrastructure.Persistence.Model.Notification;
using ECommerce.Infrastructure.Persistence.Model.UserMangement; // Adjust to your actual User Model namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.ModelConfigurations.Notification
{
    public class NotificationConfigration : IEntityTypeConfiguration<notificationsModel>
    {
        public void Configure(EntityTypeBuilder<notificationsModel> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.notificationID);

            builder.HasOne<UserDataModel>()
                   .WithMany()
                   .HasForeignKey(n => n.ReceiverId)
                   .OnDelete(DeleteBehavior.Cascade) 
                   .IsRequired();

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(n => n.Body)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(true);

            // 5. Enumerations (Stored as Integers by default)
            builder.Property(n => n.Type)
                .IsRequired();

            builder.Property(n => n.Priority)
                .IsRequired()
               .HasDefaultValue(NotificationPriority.Normal); 

            // 6. Status & Boolean Flags
            builder.Property(n => n.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(n => n.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // 7. Audit Timestamps
            builder.Property(n => n.CreatedAt)
                .IsRequired();

            builder.Property(n => n.UpdatedAt)
                .IsRequired();

            builder.Property(n => n.ReadAt)
                .IsRequired(false); // Nullable: only set when IsRead is true

            builder.Property(n => n.DeletedAt)
                .IsRequired(false); // Nullable: only set when IsDeleted is true

            builder.HasIndex(n => new { n.ReceiverId, n.IsRead, n.IsDeleted })
                   .HasDatabaseName("IX_Notification_Receiver_Status");

            // Single index for general lookups by ID or creation date
            builder.HasIndex(n => n.CreatedAt);
        }
    }
}
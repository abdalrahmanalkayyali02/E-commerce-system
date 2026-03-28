using ECommerce.Infrastructure.Persistence.Model.UserMangement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.ModelConfigurations.UserMangement
{
    public class SellerConfigration : IEntityTypeConfiguration<SellerDataModel>
    {
        public void Configure(EntityTypeBuilder<SellerDataModel> builder)
        {
            // 1. Table Mapping
            builder.ToTable("Sellers", "iac");

            // 2. Primary Key
            builder.HasKey(s => s.sellerID);

            // 3. Property Constraints
            builder.Property(s => s.shopName)
                .IsRequired()
                .HasMaxLength(100); // Increased from 10 to 100 for realistic shop names

            builder.Property(s => s.shopPhoto)
                .IsRequired(false); // Allow nullable if they haven't uploaded yet

            builder.Property(s => s.address)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.isVerifiedByAdmin)
                .HasDefaultValue(false);

            builder.Property(s => s.verfiedSellerDocument)
                .IsRequired(false);

            builder.Property(s => s.verfiedShopDocument)
                .IsRequired(false);

            builder.Property(s => s.isVerfiedSellerDocumentBeenViewed)
                .HasDefaultValue(false);

            builder.Property(s => s.isVerfiedShopDocumentBeenViewed)
                .HasDefaultValue(false);

            builder.Property(s => s.CreateAt)
                .IsRequired();

            builder.Property(s => s.UpdateAt)
                .IsRequired();

            //FK
            builder.HasOne<UserDataModel>()
                .WithOne()
                .HasForeignKey<SellerDataModel>(s => s.sellerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
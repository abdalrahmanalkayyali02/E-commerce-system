using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Model;

namespace WebApplication1.Data.ModelConfigration;

public class SellerConfigration : IEntityTypeConfiguration<SellerDataModel>
{
    public void Configure(EntityTypeBuilder<SellerDataModel> builder)
    {
        // 1. Table Mapping
        builder.ToTable("Sellers");

        // 2. Primary Key
        builder.HasKey(s => s.SellerId);

        builder.Property(s => s.ShopName)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(s => s.ShopPhoto)
            .IsRequired(false); 

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
            .HasForeignKey<SellerDataModel>(s => s.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

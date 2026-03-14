using IAC.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAC.Infrastructure.Persistence.Seed;

public class DataSeeder : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        // 1. Generate consistent GUIDs so they don't change every time you run migrations
        var adminId = new Guid("8f395610-6302-4688-9d41-e945c71d60f4");
        var sellerId = new Guid("11111111-1111-1111-1111-111111111111");

        // 2. Seed User Data
        builder.HasData(
            new UserModel
            {
                Id = adminId,
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin",
                Email = "admin@iac.com",
                PhoneNumber = "123456789",
                PasswordHash = "AQAAAAEAACcQAAAAE...[YourHash]", // Ideally use a hashed string
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = 1, // Assume 1 = Admin
                AccountStatus = 1, // Assume 1 = Active
                IsEmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new UserModel
            {
                Id = sellerId,
                FirstName = "John",
                LastName = "Seller",
                UserName = "john_shop",
                Email = "john@shop.com",
                PhoneNumber = "987654321",
                PasswordHash = "HashedPasswordHere",
                DateOfBirth = new DateTime(1985, 5, 20),
                Role = 2, // Assume 2 = Seller
                AccountStatus = 1,
                IsEmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}

public class SellerSeeder : IEntityTypeConfiguration<SellerModel>
{
    public void Configure(EntityTypeBuilder<SellerModel> builder)
    {
        builder.HasData(new SellerModel
        {
            // Note: SellerID should match the UserModel Id if it's a 1-to-1 extension
            SellerID = new Guid("11111111-1111-1111-1111-111111111111"),
            ShopName = "Tech Haven",
            ShopPhoto = "default_shop.png",
            Address = "123 Main St, Tech City",
            IsVerifiedByAdmin = true,
            VerifiedSellerDocument = "doc_id_001.pdf",
            VerifiedShopDocument = "license_001.pdf",
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow
        });
    }
}
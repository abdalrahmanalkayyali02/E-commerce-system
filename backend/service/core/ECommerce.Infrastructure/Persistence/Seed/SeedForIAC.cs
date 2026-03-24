using Common.Enum;
using ECommerce.Infrastructure.Persistence.Model.IAC;
using System;
using System.Collections.Generic;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public static class SeedForIAC
    {
        // 1. تاريخ ميلاد ثابت (DateOnly)
        private static readonly DateOnly StaticBirthDate = new DateOnly(2000, 1, 1);

        // 2. سجلات زمنية ثابتة (UTC) لضمان عدم حدوث Kind=Local Error
        private static readonly DateTime StaticDateTimeUtc = DateTime.SpecifyKind(
            new DateTime(2026, 3, 19, 0, 0, 0),
            DateTimeKind.Utc
        );

        public static IEnumerable<UserDataModel> GetUsers()
        {
            var users = new List<UserDataModel>();

            for (int i = 1; i <= 10; i++)
            {
                var userId = Guid.Parse($"00000000-0000-0000-0000-0000000000{i:D2}");
                users.Add(new UserDataModel
                {
                    id = userId,
                    FirstName = $"User{i}",
                    LastName = "Test",
                    UserName = $"username{i}",
                    Email = $"user{i}@example.com",
                    IsEmailConfirmed = true,
                    phoneNumber = $"079000000{i:D2}",
                    password = "HashedPassword123!",

                    // تخزين DateOnly في الـ Model (سليم جداً لـ Postgres Date)
                    //DateOfBirth = StaticBirthDate.AddYears(-i),

                    Role = i <= 2 ? UserRole.Admin : (i <= 6 ? UserRole.Customer : UserRole.Seller),
                    AccountStatus = AccountStatus.Active,
                    profilePhoto = "default.jpg",

                    CreateAt = StaticDateTimeUtc,
                    UpdateAt = StaticDateTimeUtc,
                    isDelete = false,
                    // بما أننا جعلنا DeleteAt في الـ Model "Nullable"، الأفضل تمرير null للمستخدمين الفعالين
                    DeleteAt = null
                });
            }
            return users;
        }

        public static IEnumerable<CustomerDataModel> GetCustomers()
        {
            var customers = new List<CustomerDataModel>();
            for (int i = 3; i <= 6; i++)
            {
                customers.Add(new CustomerDataModel
                {
                    CustomrID = Guid.Parse($"00000000-0000-0000-0000-0000000000{i:D2}"),
                    Address = $"{i} Main St, Amman, Jordan",
                    CreateAt = StaticDateTimeUtc,
                    UpdateAt = StaticDateTimeUtc
                });
            }
            return customers;
        }

        public static IEnumerable<SellerDataModel> GetSellers()
        {
            var sellers = new List<SellerDataModel>();
            for (int i = 7; i <= 10; i++)
            {
                sellers.Add(new SellerDataModel
                {
                    sellerID = Guid.Parse($"00000000-0000-0000-0000-0000000000{i:D2}"),
                    shopName = $"Shop No {i}",
                    shopPhoto = "shop.jpg",
                    address = $"Market St {i}",
                    isVerifiedByAdmin = i % 2 == 0,
                    CreateAt = StaticDateTimeUtc,
                    UpdateAt = StaticDateTimeUtc
                });
            }
            return sellers;
        }
    }
}
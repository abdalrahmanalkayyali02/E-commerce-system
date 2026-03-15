using IAC.Domain.Entity;
using IAC.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
[assembly: InternalsVisibleTo("ECommerce.Infrastructure")]


namespace IAC.Domain.AggregateRoot
{
    public  class SellerAggregate
    {
        public Guid sellerID { get;private set;  }

        public string shopName { get; private set; }
        public string shopPhoto { get; private set; }
        public Address address { get; private set; }
        public bool isVerifiedByAdmin { get; private set; } = false;

        public string verfiedSellerDocument { get; private set; }
        public string verfiedShopDocument { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }


        private SellerAggregate() { }
        private  SellerAggregate
            (Guid id, string shopName, string shopPhoto, Address address, bool isVerfiedByAdmin,
            string verfiedShopDocument, string VerfiedSelllerDocumentt)
        {
            sellerID = id;
            this.shopName = shopName;
            this.shopPhoto = shopPhoto;
            this.address = address;
            isVerifiedByAdmin = isVerfiedByAdmin;
            this.verfiedShopDocument = verfiedShopDocument;
            verfiedSellerDocument = verfiedSellerDocument;
        }

        public static SellerAggregate Create 
            (Guid id, string shopName, string shopPhoto, Address address, bool isVerfiedByAdmin,
            string verfiedShopDocument, string VerfiedSelllerDocument)
        {
            var seller = new SellerAggregate(id, shopName, shopPhoto, address, isVerfiedByAdmin, verfiedShopDocument, VerfiedSelllerDocument);

            return seller;
        }



        public void updateShopName(string shopName)
        {
            if (shopName == this.shopName) return;  

            if (shopName.Length < 5 && shopName.Length > 10)
            {
                throw new Exception("the shop name must be between 5-10 charchter");
            }

            this.shopName = shopName;
            this.UpdateAt = DateTime.UtcNow;
        }

        public void updateShopPhoto(string  shopPhoto)
        {
            this.shopPhoto = shopPhoto;
            this.UpdateAt = DateTime.UtcNow;

        }

        public void updateShopAddress(Address address)
        {
            this.address = address;
            this.UpdateAt = DateTime.UtcNow;

        }

        public void markAsVerfied()
        {
            isVerifiedByAdmin = true;
            this.UpdateAt = DateTime.UtcNow;

        }

        public void markAsUnvervied()
        {
            isVerifiedByAdmin = false;
            this.UpdateAt = DateTime.UtcNow;

        }


        public void updateVerfiedSellerDocument(string verfiedSellerDocument)
        { 
            if (isVerifiedByAdmin == true) 
            {
                throw new InvalidOperationException("Cannot update verified seller document when the seller is already verified.");
            }
            this.verfiedSellerDocument = verfiedSellerDocument;
            UpdateAt = DateTime.UtcNow;
        }

        public void updateVerfiedShopDocument(string verfiedShopDocument)
        {
            if (isVerifiedByAdmin == true)
            {
                throw new InvalidOperationException("Cannot update verified shop document when the seller is already verified.");
            }
            this.verfiedShopDocument = verfiedShopDocument;
            UpdateAt = DateTime.UtcNow;
        }






    }
}

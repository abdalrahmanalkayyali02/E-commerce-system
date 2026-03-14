using IAC.Domain.Entity;
using IAC.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

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



        public void updateShopName(string shopName)
        {
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

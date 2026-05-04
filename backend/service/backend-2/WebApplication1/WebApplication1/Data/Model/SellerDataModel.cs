using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Data.Model
{
    public  class SellerDataModel
    {
        public Guid SellerId { get;  set; }

        public required string ShopName { get;  set; }
        public required string ShopPhoto { get;  set; }
        public string address { get;  set; }
        public required bool isVerifiedByAdmin { get;  set; } = false;

        public required string verfiedSellerDocument { get;  set; }
        public required string verfiedShopDocument { get;  set; }
        public required bool isVerfiedSellerDocumentBeenViewed { get; set; }
        public required  bool isVerfiedShopDocumentBeenViewed { get; set; }
        public DateTime CreateAt { get;  set; }
        public DateTime UpdateAt { get;  set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Data.Model
{
    public  class SellerDataModel
    {
        public Guid SellerId { get;  set; }

        public string ShopName { get;  set; }
        public string ShopPhoto { get;  set; }
        public string address { get;  set; }
        public bool isVerifiedByAdmin { get;  set; } = false;

        public string verfiedSellerDocument { get;  set; }
        public string verfiedShopDocument { get;  set; }
        public bool isVerfiedSellerDocumentBeenViewed { get; set; }
        public bool isVerfiedShopDocumentBeenViewed { get; set; }
        public DateTime CreateAt { get;  set; }
        public DateTime UpdateAt { get;  set; }
    }
}

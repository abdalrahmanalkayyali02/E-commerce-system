using IAC.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model
{
    public  class SellerDataModel
    {
        public Guid sellerID { get;  set; }

        public string shopName { get;  set; }
        public string shopPhoto { get;  set; }
        public Address address { get;  set; }
        public bool isVerifiedByAdmin { get;  set; } = false;

        public string verfiedSellerDocument { get;  set; }
        public string verfiedShopDocument { get;  set; }
        public DateTime CreateAt { get;  set; }
        public DateTime UpdateAt { get;  set; }
    }
}

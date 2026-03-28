using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model.UserMangement
{
    public class CustomerDataModel
    {
        public Guid CustomrID { get;  set; }
        public string Address { get;  set; }

        public DateTime CreateAt { get;  set; }
        public DateTime UpdateAt { get;  set; }
    }
}

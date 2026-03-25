using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.Modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class SellerByIdSpecfication : BaseSpecification<SellerEntity>
    {
        public SellerByIdSpecfication(Guid id)
            : base(u => u.sellerID == id)
        { }

    }
}
    

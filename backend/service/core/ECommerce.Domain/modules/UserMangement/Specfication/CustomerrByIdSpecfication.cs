using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.UserMangement.Specfication
{
    public class CustomerrByIdSpecfication : BaseSpecification<CustomerEntity>
    {

        public CustomerrByIdSpecfication(Guid id)
            : base(u => u.CustomrID == id)
        { }
    }
}

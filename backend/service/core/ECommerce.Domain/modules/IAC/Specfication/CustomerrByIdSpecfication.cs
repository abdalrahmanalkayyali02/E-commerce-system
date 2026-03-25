using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.Modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class CustomerrByIdSpecfication : BaseSpecification<CustomerEntity>
    {

        public CustomerrByIdSpecfication(Guid id)
            : base(u => u.CustomrID == id)
        { }
    }
}

using Common.Specfication;
using ECommerce.Domain.Modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class UserByIdSpecfication : BaseSpecification<UserEntity>
    {
        public UserByIdSpecfication(Guid id) 
            :base(u => u.Id == id)
        { }
    }
}

using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.UserMangement.Specfication
{
    public class UserByIdSpecfication : BaseSpecification<UserEntity>
    {
        public UserByIdSpecfication(Guid id) 
            :base(u => u.Id == id)
        { }
    }
}

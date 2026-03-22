using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.Modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class UserByUserNameSpecfication : BaseSpecification<UserEntity>
    {

        public UserByUserNameSpecfication(string username, Guid? id = null) :
            base(u => u.UserName.Value == username && (id == null || u.Id != id))
        {}
    }
}

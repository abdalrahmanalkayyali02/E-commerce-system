using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.UserMangement.Specfication
{
    public class UserByUserNameSpecfication : BaseSpecification<UserEntity>
    {

        public UserByUserNameSpecfication(string username, Guid? id = null) :
            base(u => u.UserName.Value == username && (id == null || u.Id != id))
        {}
    }
}

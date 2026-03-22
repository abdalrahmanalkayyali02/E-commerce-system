using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.Modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class UserByPhoneNumberSpecfication : BaseSpecification<UserEntity>
    {
        public UserByPhoneNumberSpecfication(string phoneNumber, Guid? id = null)
            : base(u => u.PhoneNumber.Value == phoneNumber && (id == null || u.Id != id))
        { }
    }
}

using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.Modules.IAC.Entity;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class UserByEmailSpecification : BaseSpecification<UserEntity>
    {
        public UserByEmailSpecification(string email, Guid? id = null)
            : base(u => u.Email.Value == email
                        && !u.IsDeleted
                        && (id == null || u.Id != id)) 
        {
            
        }
    }
}
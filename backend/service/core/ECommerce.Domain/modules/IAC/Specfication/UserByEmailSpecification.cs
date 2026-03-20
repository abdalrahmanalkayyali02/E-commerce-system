using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class UserByEmailSpecification : BaseSpecification<UserEntity>
    {
        public UserByEmailSpecification(string email, Guid? id = null)
            : base(u => u.Email.Value == email
                        && !u.isDelete
                        && (id == null || u.Id != id)) 
        {
            
        }
    }
}
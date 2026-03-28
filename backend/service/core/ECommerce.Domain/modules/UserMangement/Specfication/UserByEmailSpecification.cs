using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;

namespace ECommerce.Domain.modules.UserMangement.Specfication
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
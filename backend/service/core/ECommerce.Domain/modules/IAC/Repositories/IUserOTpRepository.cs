using ECommerce.Domain.modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Repositories
{
    public interface IUserOTpRepository
    {
        public Task AddAsync(UserOTPEntity userOTPEntity,CancellationToken cancellationToken = default);
        public void Update (UserOTPEntity userOTPEntity, CancellationToken cancellationToken = default);

        public void SoftDelete (UserOTPEntity userOTPEntity,CancellationToken cancellation = default);
        // get otp by userid, and type // and check if the otp is valid (not expired and not used) for last one 
        // if last otp for forget password and other is active but last one is verfied the other with same type should be invalid
        // if last otp for forget password and other is active but last one is not verfied the other with same type should be invalid
        // if last otp for forget password and other is active but last one is expired the other with same type should be invalid
        // if last otp for forget password and other is active but last one is used the other with same type should be invalid
        // if last otp for forget password and other is active but last one is not expired and not used the other with same type should be valid
        // if last otp for forget password and other is active but last one is not expired and not used but failed attempts more than 3 the other with same
        // type should be invalid



    }
}

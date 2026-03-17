using BCrypt.Net;
using ECommerce.Application.Abstraction.IExternalService;
using System;
using System.Collections.Generic;
using System.Text;


namespace ECommerce.Infrastructure.ExternalService
{
    public class BCryptPasswordService : IPasswordService
    {
        public  string PasswordHash(string password)
        {
           return  BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool PasswordVerify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}

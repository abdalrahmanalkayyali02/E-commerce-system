using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IAC.Application.Abstraction
{
    public interface IPasswordService
    {
        public string PasswordHash(string password);  
        public bool PasswordVerify(string password, string hash);
    }
}

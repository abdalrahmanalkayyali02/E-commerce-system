using IAC.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.Abstraction
{
    public interface IEmailGatway
    {
        Task SendOtpEmailAsync(string userEmail, string otp, EmailOtpType type);
    }
}



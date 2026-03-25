using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Abstraction.IExternalService
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(string userEmail, string otp, OtpType type);
    }
}



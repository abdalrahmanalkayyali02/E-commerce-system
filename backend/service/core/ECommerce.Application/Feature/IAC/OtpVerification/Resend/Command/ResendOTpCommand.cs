using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.OtpVerification.Resend.Command
{
    public record ResendOTpCommand(string email) : IRequest<Result<string>>;
    
    
}

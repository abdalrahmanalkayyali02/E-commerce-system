using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.OtpVerification.Resend.Command
{
    public record ResendOTpCommand(string email) : IRequest<Result<string>>;
    
    
}

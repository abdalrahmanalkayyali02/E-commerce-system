using Common.Enum;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.OtpVerification.Verified.Command
{
    public record ResendOtpCommand(string email, OtpType type):IRequest<Result<string>>;
    
    
}

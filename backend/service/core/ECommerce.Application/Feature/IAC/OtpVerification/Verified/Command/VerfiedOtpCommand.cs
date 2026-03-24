using Common.DTOs.IAC.OTpVerfrication;
using Common.Impl.Result;
using ECommerce.Domain.modules.IAC.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.OtpVerification.Verified.Command
{
    public record VerfiedOtpCommand(string email, string otp) : IRequest<Result<VerfiedOtpResponse>>;
    
    
}

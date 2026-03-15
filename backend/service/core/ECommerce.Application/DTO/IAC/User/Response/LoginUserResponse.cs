using IAC.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.IAC.User.Response
{

    public record LoginUserResponse(string? Token, bool? IsEmailVerified,AccountStatus? AccountStatus, string Message)
    {
        public static LoginUserResponse Success(string token, bool verified, AccountStatus status)
            => new(token, verified, status, "Success");

        // Helper for Error
        public static LoginUserResponse Failure(string errorMessage)
            => new(null, null, null, errorMessage);
    }

}

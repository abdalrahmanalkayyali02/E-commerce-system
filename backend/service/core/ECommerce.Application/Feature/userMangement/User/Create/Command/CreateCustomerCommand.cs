using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Common.Impl.Result;
using Common.DTOs.IAC.User;

namespace ECommerce.Application.Feature.userMangement.User.Create.Command
{
    public record CreateCustomerCommand
    (
        string firstName, string lastName, string userName,
        string dateOfBirth, string email, string phoneNumber,
        string password, string? profilePhoto, string address
    ) : IRequest<Result<CreateUserResponse>>;
    
}

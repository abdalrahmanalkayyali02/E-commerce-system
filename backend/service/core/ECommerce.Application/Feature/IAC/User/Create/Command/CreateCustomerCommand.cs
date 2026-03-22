using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Common.DTOs.Catalog.Catogry.Response;
using Common.DTOs.IAC.Response;
using Common.Impl.Result;

namespace ECommerce.Application.Feature.IAC.User.Create.Command
{
    public record CreateCustomerCommand
    (
        string firstName, string lastName, string userName,
        string dateOfBirth, string email, string phoneNumber,
        string password, string? profilePhoto, string address
    ) : IRequest<Result<CreateUserResponse>>;
    
}

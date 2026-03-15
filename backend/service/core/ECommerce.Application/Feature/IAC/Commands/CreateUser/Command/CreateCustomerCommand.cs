using ECommerce.Application.DTO.IAC.User.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ECommerce.Application.Feature.IAC.Commands.CreateUser.Command
{
    public record CreateCustomerCommand
    (
        string firstName, string lastName, string userName,
        string dateOfBirth, string email, string phoneNumber,
        string password, string? profilePhoto, string address
    ) : IRequest<CreateUserResponse>;
    
}

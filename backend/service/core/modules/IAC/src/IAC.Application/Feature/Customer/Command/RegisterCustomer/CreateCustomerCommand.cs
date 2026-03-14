using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.Feature.Customer.Command.RegisterCustomer
{

    public record RegisterCustomerResult(string message, Guid customerID);
    public record CreateCustomerCommand
        (
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        string DateOfBirth,
        string Password,
        string Address
        ) : IRequest<RegisterCustomerResult>;



}

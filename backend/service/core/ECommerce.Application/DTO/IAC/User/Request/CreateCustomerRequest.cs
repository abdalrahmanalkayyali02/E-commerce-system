using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.IAC.User.Request
{
    public record CreateCustomerRequest
        (
            string firstName, string lastName, string userName,
            string dateOfBirth,string email,string phoneNumber,
            string password,string? profilePhoto, string address
        );
    
    
}

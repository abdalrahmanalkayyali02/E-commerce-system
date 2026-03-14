using IAC.Application.Contract.Create_User.Response;
using IAC.Application.DTO.Create_User.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.Feature.User.Command.CreateUser
{
    public record RegisterSellerCommand
    (
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        string DateOfBirth,
        string Password,
        string Address,
        string shopName,
        string shopPhoto,
        bool isVerifiedByAdmin,
        string verfiedSellerDocument,
        string verfiedShopDocument
    ) : IRequest<RegisterSellerRespons>;
    
}

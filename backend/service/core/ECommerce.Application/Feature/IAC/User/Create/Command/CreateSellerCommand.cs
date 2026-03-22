using Common.DTOs.IAC.Response;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.User.Create.Command
{
    public record CreateSellerCommand
    (
        string firstName, string lastName, string userName, 
        string dateOfBirth,string email, string phoneNumber,
        string password, string? profilePhoto, string address,
        string shopName, string? shopPhoto,
        string verfiedSellerDocument, string verfiedShopDocument
    ) : IRequest<Result<CreateUserResponse>>;
}



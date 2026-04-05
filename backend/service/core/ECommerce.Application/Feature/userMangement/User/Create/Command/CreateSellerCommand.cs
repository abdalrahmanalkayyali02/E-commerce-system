using Common.DTOs.UserMangement.User;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.User.Create.Command
{
    public record CreateSellerCommand
    (
        string firstName, string lastName, string userName, 
        string dateOfBirth,string email, string phoneNumber,
        string password, Stream? profilePhoto, string address,
        string shopName, Stream shopPhoto,
        Stream verfiedSellerDocument, Stream verfiedShopDocument
    ) : IRequest<Result<CreateUserResponse>>;
}



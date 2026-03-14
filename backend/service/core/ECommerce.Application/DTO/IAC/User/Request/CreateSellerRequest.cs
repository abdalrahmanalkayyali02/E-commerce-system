using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.IAC.User.Request
{
    public record CreateSellerRequest
    (
            string firstName, string lastName, string userName,
            string dateOfBirth, string email, string phoneNumber,
            string password, string? profilePhoto, string address,
            string shopName, string? shopPhoto, 
            string verfiedSellerDocument, string verfiedShopDocument
    );
    
}

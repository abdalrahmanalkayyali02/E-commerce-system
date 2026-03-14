using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.DTO.Create_User.Request
{
    public record RegisterSellerRequest
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
    );
}

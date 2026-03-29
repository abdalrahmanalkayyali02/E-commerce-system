using Common.DTOs.UserMangement.User;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command
{
    public record  customerProfileCommand
      (
         Guid userID,
         string? FirstName,
         string? LastName,
         string? phoneNumber,
         string? profilePhoto,
         string?  address
      ) : IRequest<Result<UpdateUserProfileResponse>>;

}

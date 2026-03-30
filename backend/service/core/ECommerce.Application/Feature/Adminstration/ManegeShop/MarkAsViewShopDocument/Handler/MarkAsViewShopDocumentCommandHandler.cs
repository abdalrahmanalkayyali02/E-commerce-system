using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Adminstration.ManegeShop.MarkAsViewSellerDocument.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeShop.MarkAsViewShopDocument.Handler
{
    public sealed class MarkAsViewShopDocumentCommandHandler : IRequestHandler<MarkAsViewSellerDocumentCommand,Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public MarkAsViewShopDocumentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<Result<bool>> Handle(MarkAsViewSellerDocumentCommand command,CancellationToken cancellationToken)
        {
            try
            {
                var adminExist = await _unitOfWork.Users.GetUserByID(command.adminId, cancellationToken);
                if (adminExist is null)
                {
                    return Result<bool>.Failure(UserIdAppError.NotFound);
                }

                if (adminExist.userType is not  UserType.Admin)
                {
                    return Result<bool>.Failure(Error.Forbidden("Access.Denied", "This operation is allowed only for admin privilege"));
                }

                var sellerExist = await _unitOfWork.Seller.GetSellerWithID(command.SellerId, cancellationToken);
                if (sellerExist is null)
                {
                    return Result<bool>.Failure(UserIdAppError.NotFound);
                }

                sellerExist.markAsViewForShopDocument();

                return Result<bool>.Success(sellerExist.isVerfiedShopDocumentBeenViewed);
            } 
            catch (Exception ex)
            {
                return Result<bool>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}

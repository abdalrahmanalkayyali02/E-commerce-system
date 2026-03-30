using Common.DTOs.Adminstration.Response;
using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Adminstration.ManegeShop.VerfiedSeller.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeShop.VerfiedSeller.Handler
{
    public sealed class VerfiedSellerCommandHandler : IRequestHandler<VerfiedSellerCommand, Result<VerfiedSellerResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public VerfiedSellerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VerfiedSellerResponse>> Handle(VerfiedSellerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Validate Admin
                var adminExist = await _unitOfWork.Users.GetUserByID(command.adminId, cancellationToken);
                if (adminExist is null) return Result<VerfiedSellerResponse>.Failure(UserIdAppError.NotFound);

                if (adminExist.userType != UserType.Admin)
                {
                    return Result<VerfiedSellerResponse>.Failure(
                        Error.Forbidden("Access.Denied", "This operation is allowed only for admin privilege"));
                }

                var sellerExist = await _unitOfWork.Seller.GetSellerWithID(command.sellerID, cancellationToken);
                if (sellerExist is null) return Result<VerfiedSellerResponse>.Failure(UserIdAppError.NotFound);

                sellerExist.markAsVerfied();

                

                _unitOfWork.Seller.Update(sellerExist);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var response = new VerfiedSellerResponse(
                    sellerExist.sellerID,
                    sellerExist.isVerifiedByAdmin,
                    "Seller has been successfully verified and is now active in the store."
                );

                return Result<VerfiedSellerResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<VerfiedSellerResponse>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}


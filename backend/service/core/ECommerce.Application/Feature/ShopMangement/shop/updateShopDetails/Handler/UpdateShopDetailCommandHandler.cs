using Common.DTOs.ShopMangement;
using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.ShopMangement.shop.updateShopDetails.Command;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.ShopMangement.shop.updateShopDetails.Handler
{
    public class UpdateShopDetailCommandHandler : IRequestHandler<UpdateShopDetailCommand, Result<updateShopDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorgeService _fileStorgeService;
        private readonly IValidator<UpdateShopDetailCommand> _validator;

        public UpdateShopDetailCommandHandler(IUnitOfWork unitOfWork, IFileStorgeService fileStorgeService, IValidator<UpdateShopDetailCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _fileStorgeService = fileStorgeService;
            _validator = validator;
        }


        public async Task<Result<updateShopDetailsResponse>> Handle(UpdateShopDetailCommand command, CancellationToken cancellationToken)
        {
            // validate the command 

            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<updateShopDetailsResponse>.Failure(
                    Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
            }

            try
            {
                var userExist = await _unitOfWork.Users.GetUserByID(command.sellerID);

                if (userExist is null)
                {
                    return Result<updateShopDetailsResponse>.Failure(UserIdAppError.NotFound);
                }

                if (userExist.userType != UserType.Seller)
                {
                    return Result<updateShopDetailsResponse>.Failure(
                        Error.Validation("User.InvalidRole", "Only sellers can update shop details."));
                }

                var sellerExist = await _unitOfWork.Seller.GetSellerWithID(command.sellerID);

                if (sellerExist is null)
                    return Result<updateShopDetailsResponse>.Failure(UserIdAppError.NotFound);


                string? photoUrl = null;
                if (command.shopPhoto != null)
                {
                    photoUrl = await _fileStorgeService.UpdateAsync(command.shopPhoto, sellerExist.shopPhoto);
                }

                var sellerDocUrl = await _fileStorgeService.UpdateAsync(command.verfiedSellerDocument!, sellerExist.verfiedSellerDocument);
                var shopDocUrl = await _fileStorgeService.UpdateAsync(command.verfiedShopDocument!, sellerExist.verfiedShopDocument);


                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<updateShopDetailsResponse>.Success(
                    new updateShopDetailsResponse("Shop details updated successfully", sellerExist.sellerID));
            }
            catch (Exception ex)
            {
                return Result<updateShopDetailsResponse>.Failure(Error.Validation("System.Error", ex.Message));

            }
        }
    }
}

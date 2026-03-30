using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Adminstration.ManegeUser.DeleteUserByCiteria.Command;
using ECommerce.Application.Feature.Adminstration.ManegeUser.UnLockedUserByCiteria.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.UnLockedUserByCiteria.Handler
{

    public sealed class UnlockUserByCriteriaCommandHandler : IRequestHandler<UnlockUserByCriteriaQuery, Result<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnlockUserByCriteriaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDto>> Handle(UnlockUserByCriteriaQuery command, CancellationToken ct)
        {
            try
            {
                var admin = await _unitOfWork.Users.GetUserByID(command.adminID, ct);
                if (admin is null) return Result<UserDto>.Failure(UserIdAppError.NotFound);

                if (admin.userType != UserType.Admin)
                {
                    return Result<UserDto>.Failure(Error.Forbidden("Access.Denied", "This operation is allowed only for admin privilege"));
                }

                var targetUser = await _unitOfWork.Users.GetUserByCriteria(
                      command.Id,
                      command.UserName,
                      command.Email,
                      command.PhoneNumber,
                      ct);

                if (targetUser is null)
                {
                    return Result<UserDto>.Failure(Error.NotFound("User.NotFound", "The user you are trying to UnLocked does not exist."));
                }

                if (targetUser.AccountStatus != AccountStatus.LocKed)
                {
                    return Result<UserDto>.Failure(Error.Failure("User.Lock", "The user you are tring to unclock is not lock already"));
                }

                targetUser.MarkAsUnlock();

                _unitOfWork.Users.Update(targetUser);
                await _unitOfWork.SaveChangesAsync(ct);

                var response = new UserDto(
                    targetUser.Id,
                    targetUser.FirstName.Value,
                    targetUser.LastName.Value,
                    targetUser.UserName.Value,
                    targetUser.Email.Value,
                    targetUser.PhoneNumber.Value,
                    targetUser.AccountStatus,
                    targetUser.IsDeleted
                );

                return Result<UserDto>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}

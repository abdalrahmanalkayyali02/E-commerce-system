using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Collection;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Adminstration.ManegeUser.FilterUserByCriteria.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.FilterUserByCriteria.Handler
{
    public sealed class FilterUserByCriteriaQueryHandler : IRequestHandler<FilterUserByCriteriaQuery, Result<PagedList<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilterUserByCriteriaQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedList<UserDto>>> Handle(FilterUserByCriteriaQuery query, CancellationToken cancellationToken)
        {
            try
            {
                // 1. التحقق من وجود الأدمن وصلاحياته
                var admin = await _unitOfWork.Users.GetUserByID(query.adminId, cancellationToken);

                if (admin is null)
                {
                    return Result<PagedList<UserDto>>.Failure(UserIdAppError.NotFound);
                }

                if (admin.userType != UserType.Admin)
                {
                    return Result<PagedList<UserDto>>.Failure(Error.Forbidden("Access.Denied", "This operation is allowed only for admin privilege"));
                }

                var pagedUsers = await _unitOfWork.Users.GetUsersByFilterAsync(
                    query.userType,
                    query.accountStatus,
                    query.isDeleted,
                    query.pageNumber,
                    query.pageSize,
                    cancellationToken);

                var userDtos = pagedUsers.Items.Select(user => new UserDto(
                    user.Id,
                    user.FirstName.Value,
                    user.LastName.Value,
                    user.UserName.Value,
                    user.Email.Value,
                    user.PhoneNumber.Value,
                    user.AccountStatus,
                    user.IsDeleted
                )).ToList();

                var result = new PagedList<UserDto>(
                    userDtos,
                    pagedUsers.TotalCount,
                    pagedUsers.PageNumber,
                    pagedUsers.PageSize);

                return Result<PagedList<UserDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<PagedList<UserDto>>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}
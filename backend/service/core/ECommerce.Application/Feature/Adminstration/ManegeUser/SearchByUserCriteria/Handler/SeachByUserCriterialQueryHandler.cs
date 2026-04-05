using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Collection; // ضروري للتعامل مع PagedList
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Adminstration.ManegeUser.SearchByUserCriteria.Query;
using ECommerce.Domain.modules.UserMangement.Entity; 
using MediatR;
using System.Linq;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.SearchByUserCriteria.Handler
{
    public sealed class SearchByUserCriteriaQueryHandler : IRequestHandler<SearchByUserCritecalQuery, Result<PagedList<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchByUserCriteriaQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedList<UserDto>>> Handle(SearchByUserCritecalQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var adminExist = await _unitOfWork.Users.GetUserByID(query.adminId, cancellationToken);

                if (adminExist is null)
                {
                    return Result<PagedList<UserDto>>.Failure(UserIdAppError.NotFound);
                }

                if (adminExist.userType != UserType.Admin)
                {
                    return Result<PagedList<UserDto>>.Failure(Error.Forbidden("Access.Denied", "This operation is allowed only for admin privilege"));
                }


                var pagedUsers = await _unitOfWork.Users.GetUsersByCriteria(
                    query.userID,
                    query.userName,
                    query.phoneNumber,
                    query.email,
                    query.pageNumber, 
                    query.pageSize,
                    cancellationToken);

                if (pagedUsers == null || !pagedUsers.Items.Any())
                {
                    return Result<PagedList<UserDto>>.Success(new PagedList<UserDto>(new List<UserDto>(), 0, query.pageNumber, query.pageSize));
                }

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
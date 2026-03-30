using Common.Collection;
using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Query;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Handler
{
    public sealed class GetAllUserByFilterQueryHandler : IRequestHandler<GetAllUsersByFilterQuery, Result<IPagedList<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetAllUsersByFilterQuery> _validator;

        public GetAllUserByFilterQueryHandler(IUnitOfWork unitOfWork, IValidator<GetAllUsersByFilterQuery> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IPagedList<UserDto>>> Handle(GetAllUsersByFilterQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(query, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return Result<IPagedList<UserDto>>.Failure(
                        Error.Validation("User.Validation", validationResult.Errors.First().ErrorMessage));
                }

                var adminExist = await _unitOfWork.Users.GetUserByID(query.adminID);

                if (adminExist is null)
                {
                    return Result<IPagedList<UserDto>>.Failure(UserIdAppError.NotFound);
                }

                // check role

                if (adminExist.userType != UserType.Admin)
                {
                    return Result<IPagedList<UserDto>>.Failure(Error.Forbidden
                        ("Access.Denied", "This operation is allowed only for admin privilege"));
                }


            }

            catch(Exception ex)
            {
                return Result<IPagedList<UserDto>>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}

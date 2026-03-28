using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using ECommerce.Infrastructure.Persistence.Model.UserMangement;
using System;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Persistence.Mapper.UserMangement
{
    public static class UserMapper
    {
        public static UserEntity FromPersistenceToDomain(UserDataModel model)
        {
            if (model == null) return null;

            return new UserEntity(
                id: model.id,
                firstName: Name.Reconstruct(model.FirstName),
                lastName: Name.Reconstruct(model.LastName),
                userName: Name.Reconstruct(model.UserName),
                dateOfBirth: DateOfBirth.Reconstructing(model.DateOfBirth),
                email: Email.Reconstruct(model.Email),
                isEmailConfirmed: model.IsEmailConfirmed,
                phoneNumber: PhoneNumber.Reconstruct(model.phoneNumber),
                password: Password.Reconstruct(model.password),
                userType: model.Role,
                accountStatus: model.AccountStatus,
                profilePhoto: model.profilePhoto,
                createdAt: DateTime.SpecifyKind(model.CreateAt, DateTimeKind.Utc),
                updatedAt: DateTime.SpecifyKind(model.UpdateAt, DateTimeKind.Utc),
                deletedAt: model.DeleteAt.HasValue
                    ? DateTime.SpecifyKind(model.DeleteAt.Value, DateTimeKind.Utc)
                    : (DateTime?)null,
                isDeleted: model.isDelete,
                ResetPasswordAllowedUntil: model.ResetPasswordAllowedUntil
            );
        }

       
        public static Expression<Func<UserDataModel, UserEntity>> AsExpression =>
            model => new UserEntity(
                model.id,
                Name.Reconstruct(model.FirstName),
                Name.Reconstruct(model.LastName),
                Name.Reconstruct(model.UserName),
                DateOfBirth.Reconstructing(model.DateOfBirth),
                Email.Reconstruct(model.Email),
                model.IsEmailConfirmed,
                PhoneNumber.Reconstruct(model.phoneNumber),
                Password.Reconstruct(model.password),
                model.Role,
                model.AccountStatus,
                model.profilePhoto,
                model.ResetPasswordAllowedUntil,
                model.CreateAt, 
                model.UpdateAt,
                model.DeleteAt, 
                model.isDelete
            );

        // 3. Domain => Persistence
        public static UserDataModel FromDomainToPersistence(UserEntity user)
        {
            return new UserDataModel
            {
                id = user.Id,
                FirstName = user.FirstName.Value,
                LastName = user.LastName.Value,
                UserName = user.UserName.Value,
                Email = user.Email.Value,
                IsEmailConfirmed = user.IsEmailConfirmed,
                phoneNumber = user.PhoneNumber.Value,
                password = user.Password.Value,
                DateOfBirth = user.DateOfBirth.Value,
                profilePhoto = user.ProfilePhoto,
                Role = user.userType,
                AccountStatus = user.AccountStatus,
                CreateAt = DateTime.SpecifyKind(user.CreatedAt, DateTimeKind.Utc),
                UpdateAt = DateTime.SpecifyKind(user.UpdatedAt, DateTimeKind.Utc),
                DeleteAt = user.DeletedAt.HasValue
                    ? DateTime.SpecifyKind(user.DeletedAt.Value, DateTimeKind.Utc)
                    : null,
                isDelete = user.IsDeleted,
                ResetPasswordAllowedUntil = user.ResetPasswordAllowedUntil
            };
        }
    }
}
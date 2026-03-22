using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Domain.Modules.IAC.Entity;
using ECommerce.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Mapper
{
    public static class UserMapper
    {
        // Domain => Persistence
        public static UserDataModel FromDomainToPersistence(UserEntity user)
        {
            var model = new UserDataModel();

            model.id = user.Id;
            model.FirstName = user.FirstName.Value;
            model.LastName = user.LastName.Value;
            model.UserName = user.UserName.Value;
            model.Email = user.Email.Value;
            model.IsEmailConfirmed = user.IsEmailConfirmed;
            model.phoneNumber = user.PhoneNumber.Value;
            model.password = user.Password.Value;
            model.DateOfBirth = user.DateOfBirth.Value;
            //model.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth.Value, DateTimeKind.Utc);
            model.profilePhoto = user.ProfilePhoto;
            model.Role = user.Role;
            model.AccountStatus = user.AccountStatus;
            model.CreateAt = DateTime.SpecifyKind(user.CreatedAt, DateTimeKind.Utc);
            model.UpdateAt = DateTime.SpecifyKind(user.UpdatedAt, DateTimeKind.Utc);
            model.DeleteAt = user.DeletedAt.HasValue
                ? DateTime.SpecifyKind(user.DeletedAt.Value, DateTimeKind.Utc)
                : null;
            model.isDelete = user.IsDeleted;

            return model;
        }

        // Persistence => Domain
        public static UserEntity FromPersistenceToDomain(UserDataModel model)
        {
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
                role: model.Role,
                accountStatus: model.AccountStatus,
                profilePhoto: model.profilePhoto,

                createdAt: DateTime.SpecifyKind(model.CreateAt, DateTimeKind.Utc),
                updatedAt: DateTime.SpecifyKind(model.UpdateAt, DateTimeKind.Utc),
                deletedAt: model.DeleteAt.HasValue
                    ? DateTime.SpecifyKind(model.DeleteAt.Value, DateTimeKind.Utc)
                    : null,
                isDeleted: model.isDelete
            );
        }
    }
}
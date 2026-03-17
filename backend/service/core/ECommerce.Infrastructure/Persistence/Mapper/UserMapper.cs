using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.ValueObject;
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
            model.profilePhoto = user.ProfilePhoto;
            model.CreateAt = user.CreatedAt;
            model.UpdateAt = user.UpdatedAt;
            model.DeleteAt = user.DeleteAt;
            model.isDelete = user.isDelete;
            

            return model;
        }





        // Persistence => Domain

        public static UserEntity FromPersistenceToDomain(UserDataModel model)
        {
            var user = new UserEntity
                (
                    id: model.id,
                    firstName: Name.FromStrict(model.FirstName),
                    lastName: Name.FromStrict(model.LastName),
                    userName: Name.From(model.UserName),
                    dateOfBirth:  DateOfBirth.From(model.DateOfBirth.ToString()), // maybe will happen error 
                    email: Email.From(model.Email),
                    isEmailConfirmed: model.IsEmailConfirmed,
                    phoneNumber: PhoneNumber.From(model.phoneNumber),
                    password: Password.From(model.password),
                    role:  model.Role,
                    accountStatus: model.AccountStatus,
                    profilePhoto: model.profilePhoto,
                    createdAt: model.CreateAt,
                    updatedAt: model.UpdateAt,
                    DeleteAt: model.DeleteAt,
                    isDelete: model.isDelete
                );

            return user;
        }
    }
}

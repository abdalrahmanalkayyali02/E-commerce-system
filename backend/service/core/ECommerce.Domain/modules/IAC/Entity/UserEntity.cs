using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.Modules.IAC.DomainError;
using ECommerce.Domain.Modules.IAC.ValueObject;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ECommerce.Infrastructure")]

namespace ECommerce.Domain.Modules.IAC.Entity
{
    public class UserEntity
    {
        public Guid Id { get; internal set; }
        public Name FirstName { get; internal set; }
        public Name LastName { get; internal set; }
        public Name UserName { get; internal set; }
        public DateOfBirth DateOfBirth { get; internal set; }
        public Email Email { get; internal set; }
        public bool IsEmailConfirmed { get; internal set; } = false;
        public PhoneNumber PhoneNumber { get; internal set; }
        public Password Password { get; internal set; }
        public UserRole Role { get; internal set; }
        public AccountStatus AccountStatus { get; internal set; }
        public string? ProfilePhoto { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public DateTime? DeletedAt { get; internal set; }
        public bool IsDeleted { get; internal set; } = false;

        public UserOTPEntity? RegisterOTP { get; internal set; }
        public UserOTPEntity? ResetPasswordOTP { get; internal set; }

        private UserEntity() { }

        internal UserEntity(
            Guid id, Name firstName, Name lastName, Name userName,
            DateOfBirth dateOfBirth, Email email, bool isEmailConfirmed,
            PhoneNumber phoneNumber, Password password, UserRole role,
            AccountStatus accountStatus, string? profilePhoto,
            DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, bool isDeleted)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            DateOfBirth = dateOfBirth;
            Email = email;
            IsEmailConfirmed = isEmailConfirmed;
            PhoneNumber = phoneNumber;
            Password = password;
            Role = role;
            AccountStatus = accountStatus;
            ProfilePhoto = profilePhoto;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DeletedAt = deletedAt;
            IsDeleted = isDeleted;
        }

        public static UserEntity Create(
            Guid id, Name firstName, Name lastName, Name userName,
            DateOfBirth dateOfBirth, Email email, PhoneNumber phoneNumber,
            Password password, UserRole role)
        {
            var now = DateTime.UtcNow;
            return new UserEntity(
                id, firstName, lastName, userName, dateOfBirth, email,
                false, phoneNumber, password, role,
                AccountStatus.Inactive, null, now, now, null, false);
        }

        // --- Update Methods (Fixing CS0305 Error) ---

        public Result<Success> UpdateFirstName(string firstName)
        {
            var result = Name.FromStrict(firstName);
            if (result.IsError) return result.Errors;

            if (FirstName == result.Value)
                return Result<Success>.Success(new Success());

            FirstName = result.Value;
            UpdatedAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> UpdateLastName(string lastName)
        {
            var result = Name.FromStrict(lastName);
            if (result.IsError) return result.Errors;

            if (LastName == result.Value)
                return Result<Success>.Success(new Success());

            LastName = result.Value;
            UpdatedAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> UpdateEmail(string email)
        {
            var result = Email.From(email);
            if (result.IsError) return result.Errors;

            if (Email == result.Value)
                return Result<Success>.Success(new Success());

            Email = result.Value;
            IsEmailConfirmed = false;
            UpdatedAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        // --- OTP and Security Methods ---

        public void SetRegisterOTP(string code)
        {
            RegisterOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, code);
        }

        public void SetResetPasswordOTP(string code)
        {
            ResetPasswordOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, code);
        }

        public Result<Success> ConfirmEmail(string code)
        {
            if (RegisterOTP == null || !RegisterOTP.IsValid() || RegisterOTP.Code.Value != code.ToUpper())
                return Error.Validation("Auth.InvalidOTP", "Invalid or expired registration OTP.");

            RegisterOTP.MarkAsUsed();
            IsEmailConfirmed = true;
            AccountStatus = AccountStatus.Active;
            UpdatedAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> VerifyResetPassword(string code)
        {
            if (ResetPasswordOTP == null || !ResetPasswordOTP.IsValid())
                return Error.NotFound("Auth.NoResetRequest", "No valid reset request found.");

            if (ResetPasswordOTP.Code.Value != code.ToUpper())
            {
                ResetPasswordOTP.IncrementFailedAttempts();
                return Error.Validation("Auth.InvalidCode", "Invalid reset code.");
            }

            ResetPasswordOTP.MarkAsVerified();
            return Result<Success>.Success(new Success());
        }
    }
}
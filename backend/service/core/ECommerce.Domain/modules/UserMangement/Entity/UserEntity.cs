using Common.Enum;
using Common.Exceptions.BussniesLogic;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

[assembly: InternalsVisibleTo("ECommerce.Infrastructure")]

namespace ECommerce.Domain.modules.UserMangement.Entity
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
        public UserType userType { get; internal set; }
        public AccountStatus AccountStatus { get; internal set; }
        public string? ProfilePhoto { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public DateTime? DeletedAt { get; internal set; }
        public DateTime? ResetPasswordAllowedUntil { get; private set; }
        public bool IsDeleted { get; internal set; } = false;

        public UserOTPEntity? RegisterOTP { get; internal set; }
        public UserOTPEntity? ResetPasswordOTP { get; internal set; }
        //public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();


        private UserEntity() { }

        internal UserEntity(
            Guid id, Name firstName, Name lastName, Name userName,
            DateOfBirth dateOfBirth, Email email, bool isEmailConfirmed,
            PhoneNumber phoneNumber, Password password, UserType userType,
            AccountStatus accountStatus, string? profilePhoto, DateTime? ResetPasswordAllowedUntil,
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
            this.userType = userType;
            this.ResetPasswordAllowedUntil = ResetPasswordAllowedUntil;
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
            Password password, UserType userType)
        {
            var now = DateTime.UtcNow;
            return new UserEntity(
                id, firstName, lastName, userName, dateOfBirth, email,
                false, phoneNumber, password, userType,
                AccountStatus.Inactive, null,null, now, now, null, false);
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

        public Result<Success> UpdatePhoneNumber(string phoneNumber)
        {
            var result = PhoneNumber.From(phoneNumber);

            if (result.IsError)  return result.Errors;

            PhoneNumber = result.Value;
            UpdatedAt= DateTime.UtcNow;
            return Result< Success>.Success(new Success());
        }

        public Result<Success> UpdateProfilePhoto(string? profilePhoto)
        {
            if (ProfilePhoto == profilePhoto)
            {
                return Result<Success>.Success(new Success());
            }


            ProfilePhoto = profilePhoto;
            UpdatedAt = DateTime.UtcNow;

            return Result<Success>.Success(new Success());
        }

        // --- OTP and Security Methods ---


        public void SetRegisterOTP(string code)
        {
            var otp = OTP.From(code); 
            RegisterOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, otp,OtpType.registration);
        }

        public void ResendOTp(string code, OtpType type)
        {
             var otp = OTP.From(code);
            
            if (type == OtpType.registration)
            {
                RegisterOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, otp, OtpType.registration);
            }

            if (type == OtpType.forgotPassword)
            {
                ResetPasswordOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, otp, OtpType.forgotPassword);
            }
        }

        public void SetResetPasswordOTP(string code)
        {
            var otp = OTP.From(code);
            ResetPasswordOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, otp, OtpType.forgotPassword);
        }

        public void VerifyAccount()
        {
            IsEmailConfirmed = true;
            AccountStatus = AccountStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AllowPasswordReset()
        {
            ResetPasswordAllowedUntil = DateTime.UtcNow.AddMinutes(15);
            UpdatedAt = DateTime.UtcNow;
        }

        public Result<Success> CompletePasswordReset(Password newPassword)
        {
            if (ResetPasswordAllowedUntil == null || DateTime.UtcNow > ResetPasswordAllowedUntil)
            {
                return Result<Success>.Failure(OTpErrorsBL.WindowExpired);
            }

            if (Password == newPassword)
            {
                return Result<Success>.Failure(Error.Validation("Reset.SamePassword", "New password cannot be the same."));
            }

            Password = newPassword;

            ResetPasswordAllowedUntil = null;

            UpdatedAt = DateTime.UtcNow;

            return Result<Success>.Success(new Success());
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


        public void MarkAsDelete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsLock()
        {
            AccountStatus = AccountStatus.LocKed;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
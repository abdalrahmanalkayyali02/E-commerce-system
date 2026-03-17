using Common.Enum;
using ECommerce.Domain.modules.IAC.ValueObject;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ECommerce.Infrastructure")]
namespace ECommerce.Domain.modules.IAC.Entity
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
        public DateTime DeleteAt { get; internal set; }
        public bool isDelete { get; internal set; } = false;

        public UserOTPEntity? RegisterOTP { get; internal set; }
        public UserOTPEntity? ResetPasswordOTP { get; internal set; }

        private UserEntity() { }

        internal UserEntity(
            Guid id, Name firstName, Name lastName, Name userName,
            DateOfBirth dateOfBirth, Email email, bool isEmailConfirmed,
            PhoneNumber phoneNumber, Password password, UserRole role,
            AccountStatus accountStatus, string? profilePhoto,
            DateTime createdAt, DateTime updatedAt,DateTime DeleteAt, bool isDelete)
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
            this.DeleteAt = DeleteAt;
            this.isDelete = isDelete;
        }

        // factory 
        public static UserEntity Create(
            Guid id, Name firstName, Name lastName, Name userName,
            DateOfBirth dateOfBirth, Email email, PhoneNumber phoneNumber,
            Password password, UserRole role)
        {
            var now = DateTime.UtcNow;
            return new UserEntity(
                id, firstName, lastName, userName, dateOfBirth, email,
                false, phoneNumber, password, role,
                AccountStatus.Inactive, null, now, now,DateTime.MinValue,false);
        }


        public void UpdateFirstName(string firstName)
        {
            var nameVo = Name.From(firstName);
            if (FirstName == nameVo) 
                return;

            if (nameVo.Value.Contains("@") || nameVo.Value.Contains("_"))
                throw new ArgumentException("First Name cannot contain @ or _.");

            FirstName = nameVo;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateLastName(string lastName)
        {
            var nameVo = Name.From(lastName);

            if (LastName == nameVo) return;

            if (nameVo.Value.Contains("@") || nameVo.Value.Contains("_"))
                throw new ArgumentException("Last Name cannot contain @ or _.");

            LastName = nameVo;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string email)
        {    
            Email = Email.From(email);

            if (this.Email == Email) return;

            IsEmailConfirmed = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRegisterOTP(string code)
        {
            RegisterOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, OTP.From(code));
        }

        public void SetResetPasswordOTP(OTP code)
        {
            ResetPasswordOTP = UserOTPEntity.Create(Guid.NewGuid(), this.Id, code);
        }

        public void ConfirmEmail(string code)
        {
            if (RegisterOTP == null || !RegisterOTP.IsValid() || RegisterOTP.Code.Value != code.ToUpper())
                throw new Exception("Invalid or expired registration OTP.");

            RegisterOTP.MarkAsUsed();
            IsEmailConfirmed = true;
            AccountStatus = AccountStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void VerifyResetPassword(string code)
        {
            if (ResetPasswordOTP == null || !ResetPasswordOTP.IsValid())
                throw new Exception("No valid reset request found.");

            if (ResetPasswordOTP.Code.Value != code.ToUpper())
            {
                ResetPasswordOTP.IncrementFailedAttempts();
                throw new Exception("Invalid reset code.");
            }

            ResetPasswordOTP.MarkAsVerified();
        }



    }
}
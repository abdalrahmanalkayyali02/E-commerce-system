using IAC.Domain.Entity;
using IAC.Domain.Enum;
using IAC.Domain.Value_Object;

namespace IAC.Domain.AggregateRoot
{
    public class UserAggregate
    {
        public Guid Id { get; private set; }
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }
        public Name UserName { get; private set; }
        public DateOfBirth DateOfBirth { get; private set; }

        public Email Email { get; private set; }
        public bool IsEmailConfirmed { get; private set; } = false;
        public PhoneNumber PhoneNumber { get; private set; }
        public Password Password { get; private set; }

        public UserRole Role { get; private set; }
        public AccountStatus AccountStatus { get; private set; }
        public string? ProfilePhoto { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public UserOTP? RegisterOTP { get; private set; }
        public UserOTP? ResetPasswordOTP { get; private set; }

        private UserAggregate() { }

        private UserAggregate(
            Guid id, Name firstName, Name lastName, Name userName,
            DateOfBirth dateOfBirth, Email email, bool isEmailConfirmed,
            PhoneNumber phoneNumber, Password password, UserRole role,
            AccountStatus accountStatus, string? profilePhoto,
            DateTime createdAt, DateTime updatedAt)
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
        }

        public static UserAggregate Create(
            Guid id, Name firstName, Name lastName, Name userName,
            DateOfBirth dateOfBirth, Email email, PhoneNumber phoneNumber,
            Password password, UserRole role)
        {
            var now = DateTime.UtcNow;
            return new UserAggregate(
                id, firstName, lastName, userName, dateOfBirth, email,
                false, phoneNumber, password, role,
                AccountStatus.Inactive, null, now, now);
        }


        public void UpdateFirstName(string firstName)
        {
            var nameVo = Name.From(firstName);
            if (nameVo.Value.Contains("@") || nameVo.Value.Contains("_"))
                throw new ArgumentException("First Name cannot contain @ or _.");

            FirstName = nameVo;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateLastName(string lastName)
        {
            var nameVo = Name.From(lastName);
            if (nameVo.Value.Contains("@") || nameVo.Value.Contains("_"))
                throw new ArgumentException("Last Name cannot contain @ or _.");

            LastName = nameVo;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string email)
        {
            Email = Email.From(email);
            IsEmailConfirmed = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRegisterOTP(string code)
        {
            RegisterOTP = UserOTP.Create(OTP.From(code));
        }

        public void SetResetPasswordOTP(string code)
        {
            ResetPasswordOTP = UserOTP.Create(OTP.From(code));
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
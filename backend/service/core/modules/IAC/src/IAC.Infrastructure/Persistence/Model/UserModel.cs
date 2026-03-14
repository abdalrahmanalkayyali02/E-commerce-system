using IAC.Infrastructure.Persistence.Model;

public class UserModel
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; } 

    public bool IsEmailConfirmed { get; set; }
    public int Role { get; set; } 
    public int AccountStatus { get; set; }
    public string? ProfilePhoto { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // birdrection relationship for fk 
    public UserOtpModel? RegisterOTP { get; set; }
    public UserOtpModel? ResetPasswordOTP { get; set; }
}
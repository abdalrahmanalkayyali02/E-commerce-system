using WebApplication1.Shared.Enum;

namespace WebApplication1.Data.Model
{
    public class UserDataModel
    {
        public Guid id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required bool IsEmailConfirmed { get; set; }
        public required string phoneNumber { get; set; }
        public required string password { get; set; }
        public required UserType Role { get; set; }
        public required AccountStatus AccountStatus { get; set; }
        public string? profilePhoto { get; set; }


        public required DateTime CreateAt { get; set; }
        public  DateTime UpdateAt { get; set; }
        public bool isDelete { get; set; } = false;
        public DateTime? DeleteAt { get; set; }
        public DateTime? ResetPasswordAllowedUntil { get; set; }

    }
}

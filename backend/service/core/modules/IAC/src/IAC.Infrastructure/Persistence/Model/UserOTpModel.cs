namespace IAC.Infrastructure.Persistence.Model
{
    public class UserOtpModel
    {
        public Guid Id { get; set; } 
        public string Code { get; set; } 
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public bool IsVerified { get; set; }
        public int FailedAttempts { get; set; }

        public Guid UserId { get; set; }
    }
}
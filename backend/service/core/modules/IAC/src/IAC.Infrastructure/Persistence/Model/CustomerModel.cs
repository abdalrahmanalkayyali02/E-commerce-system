namespace IAC.Infrastructure.Persistence.Models
{
    public class CustomerModel
    {
        public Guid CustomerID { get; set; } 

        public string Address { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
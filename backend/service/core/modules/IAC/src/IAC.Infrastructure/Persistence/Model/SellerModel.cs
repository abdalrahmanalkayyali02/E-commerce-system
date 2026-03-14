namespace IAC.Infrastructure.Persistence.Models
{
    public class SellerModel
    {
        public Guid SellerID { get; set; }
        public string ShopName { get; set; }
        public string ShopPhoto { get; set; }

        public string Address { get; set; }

        public bool IsVerifiedByAdmin { get; set; }
        public string VerifiedSellerDocument { get; set; }
        public string VerifiedShopDocument { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
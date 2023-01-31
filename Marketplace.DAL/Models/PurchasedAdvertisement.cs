
namespace MarketplaceDAL.Models
{
    public class PurchasedAdvertisement
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
        public int PurchasedByUserId { get; set; }
        public UserInfo PurchasedByUser { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;

namespace MarketplaceBL.ModelsDTO
{
    public class PurchasedAdvertisementDTO
    {
        public int Id { get; set; }
        [Required]
        public int AdvertisementId { get; set; }
        [Required]
        public int PurchasedByUserId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}

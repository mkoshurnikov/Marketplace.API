using System.ComponentModel.DataAnnotations;

namespace MarketplaceBL.ModelsDTO
{
    public class AdvertisementPATCH
    {
        [Required]
        public int Id { get; set; }
        public string? AdvName { get; set; }
        public bool? IsActive { get; set; }
        [Range(1, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal? Price { get; set; }
        public bool? isPurchased { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} can't be 0 or null.")]
        public int? SellerId { get; set; }
        public string? Description { get; set; }
        [Range(1900, 2023, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int? YearOfManufacture { get; set; }
    }
}

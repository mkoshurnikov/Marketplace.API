using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBL.ModelsDTO
{
    public class AdvertisementDTO
    {
        public int Id { get; set; }
        [Required]
        public string AdvName { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Price { get; set; }
        public bool isPurchased { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} can't be 0 or null.")]
        public int SellerId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1900, 2023, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public Nullable<int> YearOfManufacture { get; set; }
    }
}

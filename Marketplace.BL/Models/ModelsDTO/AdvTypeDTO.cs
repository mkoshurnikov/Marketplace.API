using System.ComponentModel.DataAnnotations;

namespace MarketplaceBL.ModelsDTO
{
    public class AdvTypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string AdvTypeName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceDAL.Models
{
    public class AdvType
    {
        public AdvType()
        {
            this.Advertisements = new HashSet<Advertisement>();
        }
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string AdvTypeName { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}

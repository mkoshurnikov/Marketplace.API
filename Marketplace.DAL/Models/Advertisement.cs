using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceDAL.Models
{
    public class Advertisement
    {
        public Advertisement()
        {
            this.AdvTypes = new HashSet<AdvType>();
        }
        public int Id { get; set; }
        public string AdvName { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public bool isPurchased { get; set; }
        public int SellerId { get; set; }
        public virtual UserInfo? Seller { get; set; }
        public string Description { get; set; }
        public Nullable<int> YearOfManufacture { get; set; }
        public virtual ICollection<AdvType> AdvTypes { get; set; }
        public virtual PurchasedAdvertisement PurchasedAdvertisement { get; set; }
    }
}

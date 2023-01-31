
namespace MarketplaceDAL.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            this.Advertisements = new HashSet<Advertisement>();
            this.PurchasedAdvertisements = new HashSet<PurchasedAdvertisement>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DeliveryAdress { get; set; }
        public string City { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set;}
        public virtual ICollection<PurchasedAdvertisement> PurchasedAdvertisements { get; set; }
    }
}
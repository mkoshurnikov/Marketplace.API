using MarketplaceDAL.Models;

namespace MarketplaceDAL.Contracts
{
    public interface IPurchasedArvertisementRepository: IGenericRepository<PurchasedAdvertisement>
    {
        IEnumerable<PurchasedAdvertisement> GetByUserId(int id);
        PurchasedAdvertisement? GetByAdvertisementId(int id);
    }
}

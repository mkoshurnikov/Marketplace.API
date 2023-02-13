using MarketplaceBL.ModelsDTO;
using MarketplaceDAL.Models;

namespace Marketplace.BL.Abstractions
{
    public interface IPurchasedAdvertisementService
    {
        IEnumerable<PurchasedAdvertisementDTO> Get();
        PurchasedAdvertisementDTO Get(int id);
        IEnumerable<PurchasedAdvertisementDTO> GetByUserId(int id);
        PurchasedAdvertisementDTO GetByAdvertisementId(int id);
        PurchasedAdvertisementDTO Post(PurchasedAdvertisementDTO obj);
        PurchasedAdvertisement Put(PurchasedAdvertisement obj);
        void Delete(int id);
    }
}

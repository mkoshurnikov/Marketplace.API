using MarketplaceDAL.Models;

namespace MarketplaceDAL.Contracts
{
    public interface IAdvertisementRepository: IGenericRepository<Advertisement>
    {
        IEnumerable<Advertisement> GetActiveAdvertisements();
        IEnumerable<Advertisement> GetInnactiveAdvertisements();
        IEnumerable<Advertisement> GetAdvertisementsBySellerId(int id);
        IEnumerable<Advertisement> GetAdvertisementsByTypes(string[] typeNames);

    }
}

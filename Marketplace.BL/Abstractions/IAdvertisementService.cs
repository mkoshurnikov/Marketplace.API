using MarketplaceBL.ModelsDTO;

namespace Marketplace.BL.Abstractions
{
    public interface IAdvertisementService
    {
        IEnumerable<AdvertisementDTO> Get();
        AdvertisementDTO Get(int id);
        IEnumerable<AdvertisementDTO> Get(bool isActive);
        IEnumerable<AdvertisementDTO> GetBySellerId(int id);
        IEnumerable<AdvertisementDTO> GetByAdvTypeNames(string? advType1, string? advType2, string? advType3);
        AdvertisementDTO Post(AdvertisementDTO obj);
        AdvertisementDTO Put(AdvertisementDTO obj);
        AdvertisementPATCH Patch(AdvertisementPATCH obj);
        void Delete(int id);
    }
}
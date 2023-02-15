using MarketplaceBL.ModelsDTO;

namespace Marketplace.BL.Abstractions
{
    public interface IAdvTypeService
    {
        IEnumerable<AdvTypeDTO> Get();
        AdvTypeDTO Get(int id);
        AdvTypeDTO GetTypeByName(string name);
        AdvTypeDTO Post(AdvTypeDTO obj);
        AdvTypeDTO Put(AdvTypeDTO obj);
        void Delete(int id);
    }
}

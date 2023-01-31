using MarketplaceDAL.Models;

namespace MarketplaceDAL.Contracts
{
    public interface IAdvTypeRepository: IGenericRepository<AdvType>
    {
        AdvType? GetTypeByName(string name);
    }
}

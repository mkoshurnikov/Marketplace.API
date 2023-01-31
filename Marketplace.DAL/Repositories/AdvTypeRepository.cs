using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.UnitOfWork;

namespace MarketplaceDAL.Repositories
{
    public class AdvTypeRepository: GenericRepository<AdvType>, IAdvTypeRepository
    {
        public AdvTypeRepository(IUnitOfWork<MarketplaceDbContext> unitOfWork)
            : base(unitOfWork)
        {
        }
        public AdvTypeRepository(MarketplaceDbContext context)
            : base(context)
        {
        }

        public AdvType? GetTypeByName(string name)
        {
            return Context.AdvTypes.Where(at => at.AdvTypeName == name).FirstOrDefault();
        }
    }
}

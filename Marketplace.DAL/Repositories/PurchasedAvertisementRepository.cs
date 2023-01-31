using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.UnitOfWork;

namespace MarketplaceDAL.Repositories
{
    public class PurchasedAvertisementRepository: GenericRepository<PurchasedAdvertisement>, IPurchasedArvertisementRepository
    {
        public PurchasedAvertisementRepository(IUnitOfWork<MarketplaceDbContext> unitOfWork)
            : base(unitOfWork)
        {
        }
        public PurchasedAvertisementRepository(MarketplaceDbContext context)
            : base(context)
        {
        }

        public IEnumerable<PurchasedAdvertisement> GetByUserId(int id)
        {
            return Context.PurchasedAdvertisements.Where(u => u.Id == id).ToList();
        }
        public PurchasedAdvertisement? GetByAdvertisementId(int id)
        {
            return Context.PurchasedAdvertisements.Where(a => a.AdvertisementId == id).FirstOrDefault();
        }
    }
}

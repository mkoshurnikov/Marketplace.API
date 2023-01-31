using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceDAL.Repositories
{
    public class AdvertisementRepository: GenericRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(IUnitOfWork<MarketplaceDbContext> unitOfWork)
            : base(unitOfWork)
        {
        }
        public AdvertisementRepository(MarketplaceDbContext context)
            : base(context)
        {
        }
        public IEnumerable<Advertisement> GetActiveAdvertisements()
        {
            return Context.Advertisements.Where(a => a.IsActive == true).ToList();
        }
        public IEnumerable<Advertisement> GetInnactiveAdvertisements()
        {
            return Context.Advertisements.Where(a => a.IsActive == false).ToList();
        }
        public IEnumerable<Advertisement> GetAdvertisementsBySellerId(int id)
        {
            return Context.Advertisements.Where(a => a.SellerId == id).ToList();
        }
        public IEnumerable<Advertisement> GetAdvertisementsByTypes(string[] typeNames)
        {
            int [] typeIds = Context.AdvTypes.Where(t => typeNames.Contains(t.AdvTypeName)).Select(t => t.Id).ToArray();

            var table = Context.AdvTypes.SelectMany(a => a.Advertisements.Select(at => new { advTypeId = at.Id, advertisementId = a.Id })).ToList();
            int[] advertisementIds = table.Where(t => typeIds.Contains(t.advTypeId)).Select(a => a.advertisementId).ToArray();

            return Context.Advertisements.Where(a => advertisementIds.Contains(a.Id)).ToList();
        }
    }
}

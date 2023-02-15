using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Tests.Helpers
{
    public class MarketplaceDbFixture : IDisposable
    {
        public MarketplaceDbContext Context { get; private set; }

        public MarketplaceDbFixture()
        {
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                .UseInMemoryDatabase("InMemoryMarketplaceDb")
                .Options;

            Context = new MarketplaceDbContext(options);

            Context.AdvTypes.Add(new AdvType { Id = 1, AdvTypeName = "at1" });
            Context.AdvTypes.Add(new AdvType { Id = 2, AdvTypeName = "at2" });
            Context.AdvTypes.Add(new AdvType { Id = 3, AdvTypeName = "at3" });
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

using Marketplace.Tests.Helpers;
using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Tests
{
    public class AdvTypeToDtoTest: IClassFixture<MarketplaceDbFixture>
    {
        readonly MarketplaceDbContext Context;
        public AdvTypeToDtoTest(MarketplaceDbFixture fixture)
        {
            Context = fixture.Context;
        }
        [Fact]
        public void ConvertAdvTypeDbModelToDTO()
        {
            var advTypes = Context.AdvTypes.ToList();

            var advType1 = ModelsToDTO.AdvTypeToDTO(advTypes.First());
            var count = advTypes.Count();
            string advType2Name = advTypes.Skip(1).First().AdvTypeName;

            Assert.IsType<AdvTypeDTO>(advType1);
            Assert.Equal(3, count);
            Assert.Equal("at2", advType2Name);
        }
    }
}

using Marketplace.Test.Mocks;
using MarketplaceDAL.Models;
using Xunit;

namespace MarketplaceTest
{
    public class AdvTypeRepositoryTest
    {
        [Fact]
        public void GetTypeByNameTest()
        {
            var advTypeRepositoryMock = MockAdvTypeRepository.GetMock();

            AdvType? advType1 = advTypeRepositoryMock.Object.GetTypeByName("car");
            AdvType? advType2 = advTypeRepositoryMock.Object.GetTypeByName("moto");

            Assert.Equal("car", advType1?.AdvTypeName);
            Assert.Equal("moto", advType2?.AdvTypeName);
        }

        [Fact]
        public void GetAllTest()
        {
            var advTypeRepositoryMock = MockAdvTypeRepository.GetMock();

            List<AdvType> advTypes = advTypeRepositoryMock.Object.GetAll().ToList();

            Assert.Equal(2, advTypes.Count);
            Assert.Equal("car", advTypes.First().AdvTypeName);
        }

        [Fact]
        public void GetByIdTest()
        {
            var advTypeRepositoryMock = MockAdvTypeRepository.GetMock();

            AdvType advType = advTypeRepositoryMock.Object.GetById(2);

            Assert.Equal("moto", advType.AdvTypeName);
        }
    }
}

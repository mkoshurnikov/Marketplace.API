using MarketplaceDAL.Contracts;
using MarketplaceDAL.Models;
using Moq;

namespace Marketplace.Test.Mocks
{
    public static class MockAdvTypeRepository
    {
        public static Mock<IAdvTypeRepository> GetMock()
        {
            var mock = new Mock<IAdvTypeRepository>();

            var advTypes = new List<AdvType>()
            {
                new AdvType()
                {
                    Id = 1,
                    AdvTypeName = "car"
                },
                new AdvType()
                {
                    Id = 2,
                    AdvTypeName = "moto"
                }
            };

            mock.Setup(m => m.GetAll())
                .Returns(() => advTypes);

            mock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns((int id) => advTypes.FirstOrDefault(o => o.Id == id));

            mock.Setup(m => m.GetTypeByName(It.IsAny<string>()))
                .Returns((string advTypeName) => advTypes.FirstOrDefault(o => o.AdvTypeName == advTypeName));

            mock.Setup(m => m.Insert(It.IsAny<AdvType>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Update(It.IsAny<AdvType>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Delete(It.IsAny<AdvType>()))
                .Callback(() => { return; });

            return mock;
        }
    }
}

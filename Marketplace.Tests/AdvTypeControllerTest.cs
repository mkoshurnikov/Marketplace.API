using MarketplaceBL.ModelsDTO;
using MarketplacePL.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Marketplace.API.Services;

namespace Marketplace.Test
{
    public class AdvTypeControllerTest
    {
        [Fact]
        public void WhenGettingAllAdvTypes_ThenReturnAllAdvTypes()
        {
            var logger = new Logger<AdvTypesController>(new LoggerFactory());
            var unitOfWorkService = new UnitOfWorkService();

            var advTypeController = new AdvTypesController(logger, unitOfWorkService);

            var result = advTypeController.Get() as ObjectResult;
            var advTypes = result?.Value as IEnumerable<AdvTypeDTO>;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(3, advTypes?.Skip(2).First().Id);
            Assert.Equal("advtype2", advTypes?.Skip(1).First().AdvTypeName);
        }

        [Fact]
        public void WhenGettingAdvTypeById_ThenReturnAdvType()
        {
            var logger = new Logger<AdvTypesController>(new LoggerFactory());
            var unitOfWorkService = new UnitOfWorkService();

            var advTypeController = new AdvTypesController(logger, unitOfWorkService);

            int id = 1;
            var result = advTypeController.Get(id) as ObjectResult;
            var advType = result?.Value as AdvTypeDTO;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
            Assert.NotNull(result?.Value as AdvTypeDTO);
            Assert.Equal("advtype1", advType?.AdvTypeName);
        }
    }
}

using MarketplaceBL.ModelsDTO;
using MarketplacePL.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Marketplace.API.Services;
using Marketplace.BL.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using MarketplaceDAL.Models;

namespace Marketplace.Test
{
    public class AdvTypeControllerTest
    {
        [Fact]
        public void WhenGettingAllAdvTypes_ThenReturnAllAdvTypes()
        {
            var logger = new Logger<ServiceManager>(new LoggerFactory());
            var unitOfWorkService = new UnitOfWorkService();
            var userManager = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            var jwtService = new Mock<JwtService>(null);
            var services = new ServiceManager(logger, unitOfWorkService, userManager.Object, jwtService.Object);
            var advTypeController = new AdvTypesController(services);

            var result = advTypeController.Get();

            Assert.NotNull(result);
            Assert.Equal(3, result.Skip(2).First().Id);
            Assert.Equal("advtype2", result.Skip(1).First().AdvTypeName);
        }

        [Fact]
        public void WhenGettingAdvTypeById_ThenReturnAdvType()
        {
            var logger = new Logger<ServiceManager>(new LoggerFactory());
            var unitOfWorkService = new UnitOfWorkService();
            var userManager = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            var jwtService = new Mock<JwtService>(null);
            var services = new ServiceManager(logger, unitOfWorkService, userManager.Object, jwtService.Object);
            var advTypeController = new AdvTypesController(services);

            int id = 1;
            var result = advTypeController.Get(id) as ObjectResult;
            var advType = result?.Value as AdvTypeDTO;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
            Assert.NotNull(advType);
            Assert.IsType<AdvTypeDTO>(advType);
            Assert.Equal("advtype1", advType?.AdvTypeName);
        }
    }
}

using Marketplace.API.Services;
using Marketplace.BL.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Marketplace.BL.Services
{
    public class ServiceManager: IServiceManager
    {
        private readonly Lazy<IAdvertisementService> _lazyAdvertisementService;
        private readonly Lazy<IAdvTypeService> _lazyAdvTypeService;
        private readonly Lazy<IPurchasedAdvertisementService> _lazyPurchasedAdvertisementService;
        private readonly Lazy<IUserService> _lazyUserService;
        public ServiceManager(ILogger<ServiceManager> logger, UnitOfWorkService unitOfWorkService, UserManager<IdentityUser> userManager, 
            JwtService jwtService)
        {
            _lazyAdvertisementService = new Lazy<IAdvertisementService>(() => new AdvertisementService(logger, unitOfWorkService));
            _lazyAdvTypeService = new Lazy<IAdvTypeService>(() => new AdvTypeService(logger, unitOfWorkService));
            _lazyPurchasedAdvertisementService = new Lazy<IPurchasedAdvertisementService>(() => 
                new PurchasedAdvertisementService(unitOfWorkService));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(userManager, jwtService, logger, unitOfWorkService));
        }
        public IAdvertisementService AdvertisementService => _lazyAdvertisementService.Value;
        public IAdvTypeService AdvTypeService => _lazyAdvTypeService.Value;
        public IPurchasedAdvertisementService PurchasedAdvertisementService => _lazyPurchasedAdvertisementService.Value;
        public IUserService UserService => _lazyUserService.Value;
    }
}

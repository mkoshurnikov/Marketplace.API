
namespace Marketplace.BL.Abstractions
{
    public interface IServiceManager
    {
        IAdvertisementService AdvertisementService { get; }
        IAdvTypeService AdvTypeService { get; }
        IPurchasedAdvertisementService PurchasedAdvertisementService { get; }
        IUserService UserService { get; }
    }
}

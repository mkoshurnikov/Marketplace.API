using Marketplace.BL.Abstractions;
using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.Repositories;
using MarketplaceDAL.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Marketplace.BL.Services
{
    internal class AdvertisementService: IAdvertisementService
    {
        private readonly UnitOfWork<MarketplaceDbContext> _unitOfWork;
        private readonly IAdvertisementRepository _advRepository;
        private readonly ILogger<ServiceManager> _logger;
        public AdvertisementService(ILogger<ServiceManager> logger, UnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService.GetUnitOfWork();
            _advRepository = new AdvertisementRepository(_unitOfWork);
            _logger = logger;
        }
        public IEnumerable<AdvertisementDTO> Get()
        {
            return _advRepository.GetAll().Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }
        public AdvertisementDTO Get(int id)
        {
            var advertisement = _advRepository.GetById(id);
            if (advertisement != null)
            {
                return ModelsToDTO.AdvertisementToDTO(advertisement);
            }
            throw new Exception("Advertisement GET(id)");
        }
        public IEnumerable<AdvertisementDTO> Get(bool isActive)
        {
            if (isActive)
            {
                return _advRepository.GetActiveAdvertisements().Select(a => ModelsToDTO.AdvertisementToDTO(a));
            }
            return _advRepository.GetInnactiveAdvertisements().Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }
        public IEnumerable<AdvertisementDTO> GetBySellerId(int id)
        {
            return _advRepository.GetAdvertisementsBySellerId(id).Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }
        public IEnumerable<AdvertisementDTO> GetByAdvTypeNames(string? advType1, string? advType2, string? advType3)
        {
            string[] advTypes = new string[] { advType1, advType2, advType3 };
            return _advRepository.GetAdvertisementsByTypes(advTypes).Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }
        public AdvertisementDTO Post(AdvertisementDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                Advertisement adv = new Advertisement
                {
                    AdvName = obj.AdvName,
                    IsActive = obj.IsActive,
                    Price = obj.Price,
                    isPurchased = obj.isPurchased,
                    SellerId = obj.SellerId,
                    Description = obj.Description,
                    YearOfManufacture = obj.YearOfManufacture
                };
                _advRepository.Insert(adv);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                _logger.LogInformation("New advertisement has been added.");

                return obj;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return obj;
            }
        }
        public AdvertisementDTO Put(AdvertisementDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                Advertisement adv = new Advertisement
                {
                    Id = obj.Id,
                    AdvName = obj.AdvName,
                    IsActive = obj.IsActive,
                    Price = obj.Price,
                    isPurchased = obj.isPurchased,
                    SellerId = obj.SellerId,
                    Description = obj.Description,
                    YearOfManufacture = obj.YearOfManufacture
                };
                _advRepository.Update(adv);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                _logger.LogInformation($"Advertisement(id = {adv.Id}) has been changed.");

                return obj;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return obj;
            }
        }
        public AdvertisementPATCH Patch(AdvertisementPATCH obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                Advertisement dbEntity = _advRepository.GetById(obj.Id);
                Advertisement adv = ModelsToDTO.AdvertisementPatchToDbModel(obj, dbEntity);
                _advRepository.Update(adv);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                _logger.LogInformation($"Advertisement(id = {adv.Id} has been changed.");

                return obj;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return obj;
            }
        }
        public void Delete(int id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var entity = _advRepository.GetById(id);
                if (entity != null)
                {
                    _advRepository.Delete(entity);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();

                    _logger.LogInformation($"Advertisement(id = {id} has been deleted.");
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
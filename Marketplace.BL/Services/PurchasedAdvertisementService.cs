using Marketplace.BL.Abstractions;
using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.Repositories;
using MarketplaceDAL.UnitOfWork;

namespace Marketplace.BL.Services
{
    internal class PurchasedAdvertisementService: IPurchasedAdvertisementService
    {
        private readonly UnitOfWork<MarketplaceDbContext> _unitOfWork;
        private readonly IPurchasedArvertisementRepository _purchasedAdvRepository;
        public PurchasedAdvertisementService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService.GetUnitOfWork();
            _purchasedAdvRepository = new PurchasedAvertisementRepository(_unitOfWork);
        }
        public IEnumerable<PurchasedAdvertisementDTO> Get()
        {
            return _purchasedAdvRepository.GetAll().Select(p => ModelsToDTO.PurchasedAdvertisementToDTO(p));
        }
        public PurchasedAdvertisementDTO Get(int id)
        {
            var purchasedAdvertisement = _purchasedAdvRepository.GetById(id);
            if (purchasedAdvertisement != null)
            {
                return ModelsToDTO.PurchasedAdvertisementToDTO(purchasedAdvertisement);
            }
            throw new Exception("PurchasedAdvertisement Get(id)");
        }
        public IEnumerable<PurchasedAdvertisementDTO> GetByUserId(int id)
        {
            return _purchasedAdvRepository.GetByUserId(id).Select(p => ModelsToDTO.PurchasedAdvertisementToDTO(p));
        }
        public PurchasedAdvertisementDTO GetByAdvertisementId(int id)
        {
            PurchasedAdvertisement? purchasedAdv = _purchasedAdvRepository.GetByAdvertisementId(id);
            if (purchasedAdv != null)
            {
                return ModelsToDTO.PurchasedAdvertisementToDTO(purchasedAdv);
            }
            throw new Exception("PurchasedAdvertisement GetByUserId(id)");
        }
        public PurchasedAdvertisementDTO Post(PurchasedAdvertisementDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                PurchasedAdvertisement purchasedAdv = new PurchasedAdvertisement
                {
                    PurchasedByUserId = obj.PurchasedByUserId,
                    AdvertisementId = obj.AdvertisementId,
                    PurchaseDate = obj.PurchaseDate
                };
                _purchasedAdvRepository.Insert(purchasedAdv);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                return obj;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return obj;
            }
        }
        public PurchasedAdvertisement Put(PurchasedAdvertisement obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                PurchasedAdvertisement purchasedAdv = new PurchasedAdvertisement
                {
                    Id = obj.Id,
                    PurchasedByUserId = obj.PurchasedByUserId,
                    AdvertisementId = obj.AdvertisementId,
                    PurchaseDate = obj.PurchaseDate
                };
                _purchasedAdvRepository.Update(purchasedAdv);
                _unitOfWork.Save();
                _unitOfWork.Commit();

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
                var entity = _purchasedAdvRepository.GetById(id);
                if (entity != null)
                {
                    _purchasedAdvRepository.Delete(entity);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
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

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
    internal class AdvTypeService: IAdvTypeService
    {
        private readonly UnitOfWork<MarketplaceDbContext> _unitOfWork;
        private readonly IAdvTypeRepository _advTypeRepository;
        private readonly ILogger<ServiceManager> _logger;

        public AdvTypeService(ILogger<ServiceManager> logger, UnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService.GetUnitOfWork();
            _advTypeRepository = new AdvTypeRepository(_unitOfWork);
            _logger = logger;
        }
        public IEnumerable<AdvTypeDTO> Get()
        {
            return _advTypeRepository.GetAll().Select(at => ModelsToDTO.AdvTypeToDTO(at));
        }
        public AdvTypeDTO Get(int id)
        {
            AdvType advType = _advTypeRepository.GetById(id);
            if (advType != null)
            {
                AdvTypeDTO advTypeDTO = ModelsToDTO.AdvTypeToDTO(advType);
                return advTypeDTO;
            }
            throw new Exception("AdvType GET(id)");
        }
        public AdvTypeDTO GetTypeByName(string name)
        {
            AdvType? advType = _advTypeRepository.GetTypeByName(name);
            if (advType != null)
            {
                return ModelsToDTO.AdvTypeToDTO(advType);
            }
            throw new Exception("AdvType GetTypeByName(name)");
        }
        public AdvTypeDTO Post(AdvTypeDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                AdvType advType = new AdvType
                {
                    AdvTypeName = obj.AdvTypeName
                };

                _advTypeRepository.Insert(advType);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                _logger.LogInformation("New advertisement type has been added.");

                return obj;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return obj;
            }
        }
        public AdvTypeDTO Put(AdvTypeDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                AdvType advType = new AdvType
                {
                    Id = obj.Id,
                    AdvTypeName = obj.AdvTypeName
                };
                _advTypeRepository.Update(advType);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                _logger.LogInformation($"AdvType(id = {advType.Id} has been changed.");

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
                var advType = _advTypeRepository.GetById(id);
                if (advType != null)
                {
                    _advTypeRepository.Delete(advType);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();

                    _logger.LogInformation($"AdvType(id = {id} has been deleted.");
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

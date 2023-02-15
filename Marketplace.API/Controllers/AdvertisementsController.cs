using Marketplace.API.Services;
using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.Repositories;
using MarketplaceDAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly UnitOfWork<MarketplaceDbContext> _unitOfWork;
        private readonly IAdvertisementRepository _advRepository;
        private readonly ILogger<AdvertisementsController> _logger;
        public AdvertisementsController(ILogger<AdvertisementsController> logger, UnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService.GetUnitOfWork();
            _advRepository = new AdvertisementRepository(_unitOfWork);
            _logger = logger;
        }
        // GET: api/<AdvertisementsController>
        
        [HttpGet]
        public IEnumerable<AdvertisementDTO> Get()
        {
            return _advRepository.GetAll().Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // GET api/<AdvertisementsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advertisement = _advRepository.GetById(id);
            if (advertisement != null)
            {
                return Ok(ModelsToDTO.AdvertisementToDTO(advertisement));
            }
            return BadRequest();
        }

        // GET api/<AdvertisementsController>/isActive?isActive=true
        [HttpGet("isActive")]
        public IEnumerable<AdvertisementDTO> Get(bool isActive)
        {
            if (isActive)
            {
                return _advRepository.GetActiveAdvertisements().Select(a => ModelsToDTO.AdvertisementToDTO(a));
            }
            return _advRepository.GetInnactiveAdvertisements().Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // GET api/<AdvertisementsController>/bySellerId?id=1
        [HttpGet("bySellerId")]
        public IEnumerable<AdvertisementDTO> GetBySellerId(int id)
        {
            return _advRepository.GetAdvertisementsBySellerId(id).Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // GET api/<AdvertisementsController>/byAdvTypes?advType1=tname1&advType2=tname2&advType3=tname3
        [HttpGet("byAdvTypes")]
        public IEnumerable<AdvertisementDTO> GetByAdvTypeNames(string? advType1, string? advType2, string? advType3)
        {
            string [] advTypes = new string [] { advType1, advType2, advType3 };
            return _advRepository.GetAdvertisementsByTypes(advTypes).Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // POST api/<AdvertisementsController>
        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] AdvertisementDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
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

                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }

        // PUT api/<AdvertisementsController>
        [Authorize]
        [HttpPut]
        public ActionResult Put([FromBody] AdvertisementDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
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

                    _logger.LogInformation($"Advertisement(id = {adv.Id} has been changed.");

                    return Ok();
                }
                return BadRequest();
            }   
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        // PATCH api/<AdvertisementsController>
        [Authorize]
        [HttpPatch]
        public ActionResult Patch([FromBody] AdvertisementPATCH obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    Advertisement dbEntity = _advRepository.GetById(obj.Id);
                    Advertisement adv = ModelsToDTO.AdvertisementPatchToDbModel(obj, dbEntity);
                    _advRepository.Update(adv);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();

                    _logger.LogInformation($"Advertisement(id = {adv.Id} has been changed.");

                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        // DELETE api/<AdvertisementsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    var entity = _advRepository.GetById(id);
                    if (entity != null)
                    {
                        _advRepository.Delete(entity);
                        _unitOfWork.Save();
                        _unitOfWork.Commit();

                        _logger.LogInformation($"Advertisement(id = {id} has been deleted.");

                        return Ok();
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}

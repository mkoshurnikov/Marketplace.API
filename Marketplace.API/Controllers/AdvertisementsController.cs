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
        private UnitOfWork<MarketplaceDbContext> unitOfWork = new UnitOfWork<MarketplaceDbContext>();
        private IAdvertisementRepository advRepository;
        private ILogger<AdvertisementsController> _logger;
        public AdvertisementsController(ILogger<AdvertisementsController> logger)
        {
            advRepository = new AdvertisementRepository(unitOfWork);
            _logger = logger;
        }
        // GET: api/<AdvertisementsController>
        
        [HttpGet]
        public IEnumerable<AdvertisementDTO> Get()
        {
            return advRepository.GetAll().Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // GET api/<AdvertisementsController>/5
        [HttpGet("{id}")]
        public AdvertisementDTO Get(int id)
        {
            return ModelsToDTO.AdvertisementToDTO(advRepository.GetById(id));
        }

        // GET api/<AdvertisementsController>/isActive?isActive=true
        [HttpGet("isActive")]
        public IEnumerable<AdvertisementDTO> Get(bool isActive)
        {
            if (isActive)
            {
                return advRepository.GetActiveAdvertisements().Select(a => ModelsToDTO.AdvertisementToDTO(a));
            }
            return advRepository.GetInnactiveAdvertisements().Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // GET api/<AdvertisementsController>/bySellerId?id=1
        [HttpGet("bySellerId")]
        public IEnumerable<AdvertisementDTO> GetBySellerId(int id)
        {
            return advRepository.GetAdvertisementsBySellerId(id).Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // GET api/<AdvertisementsController>/byAdvTypes?advType1=tname1&advType2=tname2&advType3=tname3
        [HttpGet("byAdvTypes")]
        public IEnumerable<AdvertisementDTO> GetByAdvTypeNames(string? advType1, string? advType2, string? advType3)
        {
            string [] advTypes = new string [] { advType1, advType2, advType3 };
            return advRepository.GetAdvertisementsByTypes(advTypes).Select(a => ModelsToDTO.AdvertisementToDTO(a));
        }

        // POST api/<AdvertisementsController>
        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] AdvertisementDTO obj)
        {
            try
            {
                unitOfWork.CreateTransaction();
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
                    advRepository.Insert(adv);
                    unitOfWork.Save();
                    unitOfWork.Commit();

                    _logger.LogInformation("New advertisement has been added.");

                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
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
                unitOfWork.CreateTransaction();
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
                    advRepository.Update(adv);
                    unitOfWork.Save();
                    unitOfWork.Commit();

                    _logger.LogInformation($"Advertisement(id = {adv.Id} has been changed.");

                    return Ok();
                }
                return BadRequest();
            }   
            catch (Exception ex)
            {
                unitOfWork.Rollback();
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
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    Advertisement dbEntity = advRepository.GetById(obj.Id);
                    Advertisement adv = ModelsToDTO.AdvertisementPatchToDbModel(obj, dbEntity);
                    advRepository.Update(adv);
                    unitOfWork.Save();
                    unitOfWork.Commit();

                    _logger.LogInformation($"Advertisement(id = {adv.Id} has been changed.");

                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
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
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    var entity = advRepository.GetById(id);
                    if (entity != null)
                    {
                        advRepository.Delete(entity);
                        unitOfWork.Save();
                        unitOfWork.Commit();

                        _logger.LogInformation($"Advertisement(id = {id} has been deleted.");

                        return Ok();
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}

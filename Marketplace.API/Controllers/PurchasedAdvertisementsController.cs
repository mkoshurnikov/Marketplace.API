using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.Repositories;
using MarketplaceDAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedAdvertisementsController : ControllerBase
    {
        private UnitOfWork<MarketplaceDbContext> unitOfWork = new UnitOfWork<MarketplaceDbContext>();
        private IPurchasedArvertisementRepository purchasedAdvRepository;
        public PurchasedAdvertisementsController()
        {
            purchasedAdvRepository = new PurchasedAvertisementRepository(unitOfWork);
        }

        // GET: api/<PurchasedAdvertisementController>
        [Authorize]
        [HttpGet]
        public IEnumerable<PurchasedAdvertisementDTO> Get()
        {
            return purchasedAdvRepository.GetAll().Select(p => ModelsToDTO.PurchasedAdvertisementToDTO(p));
        }

        // GET api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpGet("{id}")]
        public PurchasedAdvertisementDTO Get(int id)
        {
            return ModelsToDTO.PurchasedAdvertisementToDTO(purchasedAdvRepository.GetById(id));
        }

        // GET api/<PurchasedAdvertisementController>/byUserId/5
        [Authorize]
        [HttpGet("byUserId/{id}")]
        public IEnumerable<PurchasedAdvertisementDTO> GetByUserId(int id)
        {
            return purchasedAdvRepository.GetByUserId(id).Select(p => ModelsToDTO.PurchasedAdvertisementToDTO(p));
        }

        // GET api/<PurchasedAdvertisementController>/byAdvertisementId/5
        [Authorize]
        [HttpGet("byAdvertisementId/{id}")]
        public ActionResult<PurchasedAdvertisementDTO> GetByAdvertisementId(int id)
        {
            PurchasedAdvertisement? purchasedAdv = purchasedAdvRepository.GetByAdvertisementId(id);
            if (purchasedAdv != null)
            {
                return ModelsToDTO.PurchasedAdvertisementToDTO(purchasedAdv);
            }
            return NotFound();
        }

        // POST api/<PurchasedAdvertisementController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] PurchasedAdvertisementDTO obj)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    PurchasedAdvertisement purchasedAdv = new PurchasedAdvertisement
                    {
                        PurchasedByUserId = obj.PurchasedByUserId,
                        AdvertisementId = obj.AdvertisementId,
                        PurchaseDate = obj.PurchaseDate
                    };
                    purchasedAdvRepository.Insert(purchasedAdv);
                    unitOfWork.Save();
                    unitOfWork.Commit();

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

        // PUT api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] PurchasedAdvertisement obj)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    PurchasedAdvertisement purchasedAdv = new PurchasedAdvertisement
                    {
                        Id = obj.Id,
                        PurchasedByUserId = obj.PurchasedByUserId,
                        AdvertisementId = obj.AdvertisementId,
                        PurchaseDate = obj.PurchaseDate
                    };
                    purchasedAdvRepository.Update(purchasedAdv);
                    unitOfWork.Save();
                    unitOfWork.Commit();

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

        // DELETE api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    var entity = purchasedAdvRepository.GetById(id);
                    if (entity != null)
                    {
                        purchasedAdvRepository.Delete(entity);
                        unitOfWork.Save();
                        unitOfWork.Commit();

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

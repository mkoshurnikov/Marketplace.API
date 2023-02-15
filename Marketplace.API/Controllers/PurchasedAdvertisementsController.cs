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

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedAdvertisementsController : ControllerBase
    {
        private readonly UnitOfWork<MarketplaceDbContext> _unitOfWork;
        private readonly IPurchasedArvertisementRepository _purchasedAdvRepository;
        public PurchasedAdvertisementsController(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService.GetUnitOfWork();
            _purchasedAdvRepository = new PurchasedAvertisementRepository(_unitOfWork);
        }

        // GET: api/<PurchasedAdvertisementController>
        [Authorize]
        [HttpGet]
        public IEnumerable<PurchasedAdvertisementDTO> Get()
        {
            return _purchasedAdvRepository.GetAll().Select(p => ModelsToDTO.PurchasedAdvertisementToDTO(p));
        }

        // GET api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpGet("{id}")]
        public PurchasedAdvertisementDTO Get(int id)
        {
            return ModelsToDTO.PurchasedAdvertisementToDTO(_purchasedAdvRepository.GetById(id));
        }

        // GET api/<PurchasedAdvertisementController>/byUserId/5
        [Authorize]
        [HttpGet("byUserId/{id}")]
        public IEnumerable<PurchasedAdvertisementDTO> GetByUserId(int id)
        {
            return _purchasedAdvRepository.GetByUserId(id).Select(p => ModelsToDTO.PurchasedAdvertisementToDTO(p));
        }

        // GET api/<PurchasedAdvertisementController>/byAdvertisementId/5
        [Authorize]
        [HttpGet("byAdvertisementId/{id}")]
        public ActionResult<PurchasedAdvertisementDTO> GetByAdvertisementId(int id)
        {
            PurchasedAdvertisement? purchasedAdv = _purchasedAdvRepository.GetByAdvertisementId(id);
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
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    PurchasedAdvertisement purchasedAdv = new PurchasedAdvertisement
                    {
                        PurchasedByUserId = obj.PurchasedByUserId,
                        AdvertisementId = obj.AdvertisementId,
                        PurchaseDate = obj.PurchaseDate
                    };
                    _purchasedAdvRepository.Insert(purchasedAdv);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();

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

        // PUT api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] PurchasedAdvertisement obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
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

        // DELETE api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    var entity = _purchasedAdvRepository.GetById(id);
                    if (entity != null)
                    {
                        _purchasedAdvRepository.Delete(entity);
                        _unitOfWork.Save();
                        _unitOfWork.Commit();

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

using Marketplace.BL.Abstractions;
using Marketplace.BL.Services;
using MarketplaceBL.ModelsDTO;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Repositories;
using MarketplaceDAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AdvertisementsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<AdvertisementsController>
        [HttpGet]
        public IEnumerable<AdvertisementDTO> Get()
        {
            return _serviceManager.AdvertisementService.Get();
        }

        // GET api/<AdvertisementsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var adv = _serviceManager.AdvertisementService.Get(id);

            return Ok(adv);
        }

        // GET api/<AdvertisementsController>/isActive?isActive=true
        [HttpGet("isActive")]
        public IEnumerable<AdvertisementDTO> Get(bool isActive)
        {
            return _serviceManager.AdvertisementService.Get(isActive);
        }

        // GET api/<AdvertisementsController>/bySellerId?id=1
        [HttpGet("bySellerId")]
        public IEnumerable<AdvertisementDTO> GetBySellerId(int id)
        {
            return _serviceManager.AdvertisementService.GetBySellerId(id);
        }

        // GET api/<AdvertisementsController>/byAdvTypes?advType1=tname1&advType2=tname2&advType3=tname3
        [HttpGet("byAdvTypes")]
        public IEnumerable<AdvertisementDTO> GetByAdvTypeNames(string? advType1, string? advType2, string? advType3)
        {
            return _serviceManager.AdvertisementService.GetByAdvTypeNames(advType1, advType2, advType3);
        }

        // POST api/<AdvertisementsController>
        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] AdvertisementDTO obj)
        {
            var newAdv = _serviceManager.AdvertisementService.Post(obj);

            return Ok(newAdv);
        }

        // PUT api/<AdvertisementsController>
        [Authorize]
        [HttpPut]
        public ActionResult Put([FromBody] AdvertisementDTO obj)
        {
            var editedAdv = _serviceManager.AdvertisementService.Put(obj);

            return NoContent();
        }

        // PATCH api/<AdvertisementsController>
        [Authorize]
        [HttpPatch]
        public ActionResult Patch([FromBody] AdvertisementPATCH obj)
        {
            var editedAdv = _serviceManager.AdvertisementService.Patch(obj);

            return NoContent();
        }

        // DELETE api/<AdvertisementsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _serviceManager.AdvertisementService.Delete(id);

            return NoContent();
        }
    }
}

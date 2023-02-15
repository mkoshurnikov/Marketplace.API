using Marketplace.BL.Abstractions;
using Marketplace.BL.Services;
using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedAdvertisementsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PurchasedAdvertisementsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<PurchasedAdvertisementController>
        [Authorize]
        [HttpGet]
        public IEnumerable<PurchasedAdvertisementDTO> Get()
        {
            return _serviceManager.PurchasedAdvertisementService.Get();
        }

        // GET api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var adv = _serviceManager.PurchasedAdvertisementService.Get(id);

            return Ok(adv);
        }
        // GET api/<PurchasedAdvertisementController>/byUserId/5
        [Authorize]
        [HttpGet("byUserId/{id}")]
        public IEnumerable<PurchasedAdvertisementDTO> GetByUserId(int id)
        {
            return _serviceManager.PurchasedAdvertisementService.GetByUserId(id);
        }

        // GET api/<PurchasedAdvertisementController>/byAdvertisementId/5
        [Authorize]
        [HttpGet("byAdvertisementId/{id}")]
        public IActionResult GetByAdvertisementId(int id)
        {
            var adv = _serviceManager.PurchasedAdvertisementService.GetByAdvertisementId(id);

            return Ok(adv);
        }

        // POST api/<PurchasedAdvertisementController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] PurchasedAdvertisementDTO obj)
        {
            var newAdv = _serviceManager.PurchasedAdvertisementService.Post(obj);

            return Ok(newAdv);
        }

        // PUT api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] PurchasedAdvertisement obj)
        {
            var newAdv = _serviceManager.PurchasedAdvertisementService.Put(obj);

            return NoContent();
        }

        // DELETE api/<PurchasedAdvertisementController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _serviceManager.PurchasedAdvertisementService.Delete(id);

            return NoContent();
        }
    }
}

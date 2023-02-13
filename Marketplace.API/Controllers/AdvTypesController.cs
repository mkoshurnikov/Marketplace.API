using Marketplace.BL.Abstractions;
using MarketplaceBL.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvTypesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AdvTypesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<AdvTypeController>
        [HttpGet]
        public IEnumerable<AdvTypeDTO> Get()
        {
            return _serviceManager.AdvTypeService.Get();
        }

        // GET api/<AdvTypeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advType = _serviceManager.AdvTypeService.Get(id);

            return Ok(advType);
        }

        // GET api/<AdvTypeController>/byName/name
        [Authorize]
        [HttpGet("byName/{name}")]
        public IActionResult GetTypeByName(string name)
        {
            var advType = _serviceManager.AdvTypeService.GetTypeByName(name);

            return Ok(advType);
        }

        // POST api/<AdvTypeController>
        [HttpPost]
        public IActionResult Post([FromBody] AdvTypeDTO obj)
        {
            var newAdvType = _serviceManager.AdvTypeService.Post(obj);

            return Ok(newAdvType);
        }

        // PUT api/<AdvTypeController>
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] AdvTypeDTO obj)
        {
            var editedAdvType = _serviceManager.AdvTypeService.Put(obj);

            return NoContent();
        }

        // DELETE api/<AdvTypeController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _serviceManager.AdvTypeService.Delete(id);

            return NoContent();
        }
    }
}

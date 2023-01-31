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
    public class AdvTypesController : ControllerBase
    {
        private UnitOfWork<MarketplaceDbContext> unitOfWork = new UnitOfWork<MarketplaceDbContext>();
        private IAdvTypeRepository advTypeRepository;
        private ILogger<AdvTypesController> _logger;

        public AdvTypesController(ILogger<AdvTypesController> logger)
        {
            advTypeRepository = new AdvTypeRepository(unitOfWork);
            _logger = logger;
        }

        // GET: api/<AdvTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            var advTypes = advTypeRepository.GetAll().Select(at => ModelsToDTO.AdvTypeToDTO(at));
            return Ok(advTypes);
        }

        // GET api/<AdvTypeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            AdvType advType = advTypeRepository.GetById(id);
            if (advType != null)
            {
                AdvTypeDTO advTypeDTO = ModelsToDTO.AdvTypeToDTO(advType);
                return Ok(advTypeDTO);
            }
            return NotFound();
        }

        // GET api/<AdvTypeController>/byName/name
        [Authorize]
        [HttpGet("byName/{name}")]
        public ActionResult<AdvTypeDTO> GetTypeByName(string name)
        {
            AdvType? advType = advTypeRepository.GetTypeByName(name);
            if (advType != null)
            {
                return ModelsToDTO.AdvTypeToDTO(advType);
            }
            return NotFound();
        }

        // POST api/<AdvTypeController>
        
        [HttpPost]
        public IActionResult Post([FromBody] AdvTypeDTO obj)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    AdvType advType = new AdvType
                    {
                        AdvTypeName = obj.AdvTypeName
                    };

                    advTypeRepository.Insert(advType);
                    unitOfWork.Save();
                    unitOfWork.Commit();

                    _logger.LogInformation("New advertisement type has been added.");

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

        // PUT api/<AdvTypeController>
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] AdvTypeDTO obj)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    AdvType advType = new AdvType
                    {
                        Id = obj.Id,
                        AdvTypeName = obj.AdvTypeName
                    };
                    advTypeRepository.Update(advType);
                    unitOfWork.Save();
                    unitOfWork.Commit();

                    _logger.LogInformation($"AdvType(id = {advType.Id} has been changed.");

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

        // DELETE api/<AdvTypeController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    var advType = advTypeRepository.GetById(id);
                    if (advType != null)
                    {
                        advTypeRepository.Delete(advType);
                        unitOfWork.Save();
                        unitOfWork.Commit();

                        _logger.LogInformation($"AdvType(id = {id} has been deleted.");

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

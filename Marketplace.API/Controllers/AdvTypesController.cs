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
    public class AdvTypesController : ControllerBase
    {
        private readonly UnitOfWork<MarketplaceDbContext> _unitOfWork;
        private readonly IAdvTypeRepository _advTypeRepository;
        private readonly ILogger<AdvTypesController> _logger;

        public AdvTypesController(ILogger<AdvTypesController> logger, UnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService.GetUnitOfWork();
            _advTypeRepository = new AdvTypeRepository(_unitOfWork);
            _logger = logger;
        }

        // GET: api/<AdvTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            var advTypes = _advTypeRepository.GetAll().Select(at => ModelsToDTO.AdvTypeToDTO(at));
            return Ok(advTypes);
        }

        // GET api/<AdvTypeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            AdvType advType = _advTypeRepository.GetById(id);
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
            AdvType? advType = _advTypeRepository.GetTypeByName(name);
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
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    AdvType advType = new AdvType
                    {
                        AdvTypeName = obj.AdvTypeName
                    };

                    _advTypeRepository.Insert(advType);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();

                    _logger.LogInformation("New advertisement type has been added.");

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

        // PUT api/<AdvTypeController>
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] AdvTypeDTO obj)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    AdvType advType = new AdvType
                    {
                        Id = obj.Id,
                        AdvTypeName = obj.AdvTypeName
                    };
                    _advTypeRepository.Update(advType);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();

                    _logger.LogInformation($"AdvType(id = {advType.Id} has been changed.");

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

        // DELETE api/<AdvTypeController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    var advType = _advTypeRepository.GetById(id);
                    if (advType != null)
                    {
                        _advTypeRepository.Delete(advType);
                        _unitOfWork.Save();
                        _unitOfWork.Commit();

                        _logger.LogInformation($"AdvType(id = {id} has been deleted.");

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

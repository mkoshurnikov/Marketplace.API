using Marketplace.BL.Abstractions;
using MarketplaceBL.Models.AuthenticationModels;
using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            return _serviceManager.UserService.Get();
        }

        // GET api/<UsersController>/5
        [HttpGet("{userName}")]
        public ActionResult Get(string userName)
        {
            var user = _serviceManager.UserService.Get(userName);

            return Ok(user);
        }

        // GET api/<UsersController>/byEmail/email
        [HttpGet("byEmail/{email}")]
        public ActionResult GetByEmail(string email)
        {
            var user = _serviceManager.UserService.GetByEmail(email);

            return Ok(user);
        }

        // GET api/<UsersController>/byFirstName/firstName
        [HttpGet("byFirstname/{firstname}")]
        public IEnumerable<UserDTO> GetByFirstName(string firstName)
        {
            return _serviceManager.UserService.GetByFirstName(firstName);
        }

        // GET api/<UsersController>/byLastname/lastname
        [HttpGet("byLastname/{lastname}")]
        public IEnumerable<UserDTO> GetByLastname(string lastName)
        {
            return _serviceManager.UserService.GetByFirstName(lastName);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO? userInfo)
        {
            var user = await _serviceManager.UserService.Post(userInfo);

            return Ok(user);
        }

        // POST: api/<UsersController>/BearerToken
        [HttpPost("BearerToken")]
        public async Task<ActionResult> CreateBearerToken(AuthenticationRequest request)
        {
            var token = await _serviceManager.UserService.CreateBearerToken(request);

            return Ok(token);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _serviceManager.UserService.Delete(id);

            return StatusCode(405);
        }
    }
}

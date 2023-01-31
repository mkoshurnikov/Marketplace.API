using MarketplaceBL.ModelsDTO;
using MarketplaceBL.ResourceModels;
using MarketplaceBL.Services;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.Repositories;
using MarketplaceDAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketplacePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UnitOfWork<MarketplaceDbContext> unitOfWork = new UnitOfWork<MarketplaceDbContext>();
        private IUserInfoRepository userInfoRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;
        private ILogger<UsersController> _logger;
        public UsersController(UserManager<IdentityUser> userManager, JwtService jwtService, ILogger<UsersController> logger)
        {
            userInfoRepository = new UserInfoRepository(unitOfWork);
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            return userInfoRepository.GetAll().Select(u => ModelsToDTO.UserInfoToDTO(u));
        }

        // GET api/<UsersController>/5
        [HttpGet("{userName}")]
        public ActionResult<UserDTO> Get(string userName)
        {
            UserInfo? userInfo = userInfoRepository.GetByUserName(userName);
            if (userInfo != null)
            {
                UserDTO userDTO = ModelsToDTO.UserInfoToDTO(userInfo);
                return userDTO;
            }
            return NotFound();
        }

        // GET api/<UsersController>/byEmail/email
        [HttpGet("byEmail/{email}")]
        public ActionResult<UserDTO> GetByEmail(string email)
        {
            UserInfo? userInfo = userInfoRepository.GetUserByEmail(email);
            if (userInfo != null)
            {
                UserDTO userDTO = ModelsToDTO.UserInfoToDTO(userInfo);
                return userDTO;
            }
            return NotFound();
        }

        // GET api/<UsersController>/byFirstName/firstName
        [HttpGet("byFirstname/{firstname}")]
        public ActionResult<IEnumerable<UserDTO>> GetByFirstName(string firstName)
        {
            var usersDTO = userInfoRepository.GetByUserFirstname(firstName).Select(u => ModelsToDTO.UserInfoToDTO(u)).ToList();
            if (usersDTO.Any())
            {
                return usersDTO;
            }
            return NotFound();
        }

        // GET api/<UsersController>/byLastname/lastname
        [HttpGet("byLastname/{lastname}")]
        public ActionResult<IEnumerable<UserDTO>> GetByLastname(string lastname)
        {
            var usersDTO = userInfoRepository.GetByUserFirstname(lastname).Select(u => ModelsToDTO.UserInfoToDTO(u)).ToList();
            if (usersDTO.Any())
            {
                return usersDTO;
            }
            return NotFound();
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO? user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser newUser = new IdentityUser() 
            { 
                UserName = user.UserName, 
                Email = user.Email 
            };
            
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            UserInfo userInfo = ModelsToDTO.UserDTOtoDbModel(user);
            unitOfWork.CreateTransaction();
            userInfoRepository.Insert(userInfo);
            unitOfWork.Save();
            unitOfWork.Commit();

            _logger.LogInformation($"New user with userName: {user.UserName} has been registered.");

            return Ok();
        }

        // POST: api/<UsersController>/BearerToken
        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);

            _logger.LogInformation($"New token for user with userName: {request.UserName} has been created.");

            return Ok(token);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _logger.LogWarning($"TRYING TO DELETE USER WITH Id: {id}.");

            return StatusCode(405);
        }
    }
}

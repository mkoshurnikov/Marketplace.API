using Marketplace.API.Services;
using Marketplace.BL.Abstractions;
using MarketplaceBL.Models.AuthenticationModels;
using MarketplaceBL.ModelsDTO;
using MarketplaceBL.Services;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.Repositories;
using MarketplaceDAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Marketplace.BL.Services
{
    internal class UserService: IUserService
    {
        private UnitOfWork<MarketplaceDbContext> _unitOfWork;
        private IUserInfoRepository _userInfoRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;
        private ILogger<ServiceManager> _logger;
        public UserService(UserManager<IdentityUser> userManager, JwtService jwtService, ILogger<ServiceManager> logger,
            UnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService.GetUnitOfWork();
            _userInfoRepository = new UserInfoRepository(_unitOfWork);
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
        }
        public IEnumerable<UserDTO> Get()
        {
            return _userInfoRepository.GetAll().Select(u => ModelsToDTO.UserInfoToDTO(u));
        }
        public UserDTO Get(string userName)
        {
            UserInfo? userInfo = _userInfoRepository.GetByUserName(userName);
            if (userInfo != null)
            {
                UserDTO userDTO = ModelsToDTO.UserInfoToDTO(userInfo);
                return userDTO;
            }
            throw new Exception("PurchasedAdvertisement Get(name)");
        }
        public UserDTO GetByEmail(string email)
        {
            UserInfo? userInfo = _userInfoRepository.GetUserByEmail(email);
            if (userInfo != null)
            {
                UserDTO userDTO = ModelsToDTO.UserInfoToDTO(userInfo);
                return userDTO;
            }
            throw new Exception("PurchasedAdvertisement GetByEmail(email)");
        }
        public IEnumerable<UserDTO> GetByFirstName(string firstName)
        {
            var usersDTO = _userInfoRepository.GetByUserFirstname(firstName).Select(u => ModelsToDTO.UserInfoToDTO(u)).ToList();
            if (usersDTO.Any())
            {
                return usersDTO;
            }
            throw new Exception("PurchasedAdvertisement GetByFirstName(name)");
        }
        public IEnumerable<UserDTO> GetByLastname(string lastName)
        {
            var usersDTO = _userInfoRepository.GetByUserFirstname(lastName).Select(u => ModelsToDTO.UserInfoToDTO(u)).ToList();
            if (usersDTO.Any())
            {
                return usersDTO;
            }
            throw new Exception("PurchasedAdvertisement GetByLastName(name)");
        }
        public async Task<UserDTO> Post(UserDTO? user)
        {
            IdentityUser newUser = new IdentityUser()
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Error while adding new user");
            }

            UserInfo userInfo = ModelsToDTO.UserDTOtoDbModel(user);
            _unitOfWork.CreateTransaction();
            _userInfoRepository.Insert(userInfo);
            _unitOfWork.Save();
            _unitOfWork.Commit();

            _logger.LogInformation($"New user with userName: {user.UserName} has been registered.");

            return user;
        }
        public async Task<AuthenticationResponse> CreateBearerToken(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new Exception("Error while creating new token, user == null");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Error while creating new token, incorrect password");
            }

            var token = _jwtService.CreateToken(user);

            _logger.LogInformation($"New token for user with userName: {request.UserName} has been created.");

            return token;
        }
        public void Delete(int id)
        {
            _logger.LogWarning($"TRYING TO DELETE USER WITH Id: {id}.");
        }
    }
}

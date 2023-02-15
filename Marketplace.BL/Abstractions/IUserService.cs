using MarketplaceBL.Models.AuthenticationModels;
using MarketplaceBL.ModelsDTO;

namespace Marketplace.BL.Abstractions
{
    public interface IUserService
    {
        IEnumerable<UserDTO> Get();
        UserDTO Get(string userName);
        UserDTO GetByEmail(string email);
        IEnumerable<UserDTO> GetByFirstName(string firstName);
        IEnumerable<UserDTO> GetByLastname(string lastname);
        Task<UserDTO> Post(UserDTO? user);
        Task<AuthenticationResponse> CreateBearerToken(AuthenticationRequest request);
        void Delete(int id);
    }
}

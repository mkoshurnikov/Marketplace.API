using MarketplaceDAL.Models;

namespace MarketplaceDAL.Contracts
{
    public interface IUserInfoRepository: IGenericRepository<UserInfo>
    {
        UserInfo? GetUserByEmail(string email);
        IEnumerable<UserInfo> GetByUserFirstname(string firstName);
        IEnumerable<UserInfo> GetByUserLastname(string lastName);
        IEnumerable<UserInfo> GetByAge(int age);
        UserInfo? GetByUserName(string userName);
    }
}

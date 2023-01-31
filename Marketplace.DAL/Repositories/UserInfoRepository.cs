using MarketplaceDAL.Contracts;
using MarketplaceDAL.Data;
using MarketplaceDAL.Models;
using MarketplaceDAL.UnitOfWork;

namespace MarketplaceDAL.Repositories
{
    public class UserInfoRepository: GenericRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(IUnitOfWork<MarketplaceDbContext> unitOfWork)
            : base(unitOfWork)
        {
        }
        public UserInfoRepository(MarketplaceDbContext context)
            : base(context)
        {
        }
        public UserInfo? GetUserByEmail(string email)
        {
            return Context.UsersInfo.Where(u => u.Email == email).FirstOrDefault();
        }
        public IEnumerable<UserInfo> GetByUserFirstname(string firstName)
        {
            return Context.UsersInfo.Where(u => u.FirstName == firstName).ToList();
        }
        public IEnumerable<UserInfo> GetByUserLastname(string lastName)
        {
            return Context.UsersInfo.Where(u => u.LastName == lastName).ToList();
        }
        public IEnumerable<UserInfo> GetByAge(int age)
        {
            DateTime dt = DateTime.Today;
            return Context.UsersInfo.Where(u => ((dt.Year - u.BirthDate.Year) - dt.Month > u.BirthDate.Month || dt.Month == u.BirthDate.Month && dt.Day >= u.BirthDate.Day ? 0 : 1) == age).ToList();
        }
        public UserInfo? GetByUserName(string userName)
        {
            return Context.UsersInfo.Where(u => u.UserName == userName).First();
        }
    }
}

using WishList.Domain.Services;
using WishList.Repository;

namespace WishList.Domain
{
    public class UserServices : GenericServices<Entities.User>, IUserServices
    {
        public UserServices(DataContext dbContext) : base(dbContext) { }
    }
}
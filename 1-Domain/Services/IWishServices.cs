using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Entities;

namespace WishList.Domain.Services
{
    public interface IWishServices : IGenericServices<WishLst>
    {
        Task<IEnumerable<Product>> GetWishesByUserId(int id, int pageSize = 0, int page = 0);
    }
}
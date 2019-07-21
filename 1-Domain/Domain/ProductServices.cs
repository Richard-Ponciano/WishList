using WishList.Domain.Services;
using WishList.Repository;

namespace WishList.Domain
{
    public class ProductServices : GenericServices<Entities.Product>, IProductServices
    {
        public ProductServices(DataContext dbContext) : base(dbContext) { }
    }
}
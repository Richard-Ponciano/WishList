using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WishList.Domain.Services;
using WishList.Entities;
using WishList.Repository;

namespace WishList.Domain
{
    public class WishServices : GenericServices<Entities.WishLst>, IWishServices
    {
        private readonly DataContext _dbContext;

        public WishServices(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetWishesByUserId(int id, int pageSize = 0, int page = 0)
        {
            var lst = await _dbContext.Wishes.AsNoTracking().Include(w => w.Product).Where(w => w.UserId == id)?.Select(w => w.Product).ToListAsync();

            if (pageSize > 0)
            {
                var skip = pageSize * (page - 1);
                lst = lst.Skip((skip > 1) ? skip : 0).Take(pageSize).ToList();
            }

            return lst;
        }
    }
}
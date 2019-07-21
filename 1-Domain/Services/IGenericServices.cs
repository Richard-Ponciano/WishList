using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WishList.Domain.Services
{
    public interface IGenericServices<T> where T : class
    {
        Task<List<T>> GetAllAsync(int pageSize = 0, int page = 0, Expression<Func<T, bool>> where = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> where);
        Task<T> InsertAsync(T entinty);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
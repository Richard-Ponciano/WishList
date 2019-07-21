using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WishList.Domain.Services;
using WishList.Repository;

namespace WishList.Domain
{
    public class GenericServices<T> : IGenericServices<T> where T : class
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericServices(DataContext dbContext)
        {
            _dbContext = dbContext;
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            { throw; }
            return true;
        }

        public virtual async Task<List<T>> GetAllAsync(int pageSize = 0, int page = 0, Expression<Func<T, bool>> where = null)
        {
            var lst = (where == null) 
                ? await _dbSet.AsNoTracking().ToListAsync() 
                : await _dbSet.Where(where).AsNoTracking().ToListAsync();

            if(pageSize > 0)
            {
                var skip = pageSize * (page - 1);
                lst = lst.Skip((skip > 1) ? skip : 0).Take(pageSize).ToList();
            }

            return lst;
        }
            

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> where) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(where);

        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            { throw; }
            return true;
        }
    }
}
using ClientTele.Assessment.Data.Application;
using ClientTele.Assessment.Data.Customer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientTele.Assessment.Data.Customer.Repository
{
    /// <summary>
    /// Generic repository for async CRUD operations 
    /// </summary>
    /// <typeparam name="T">Underlying Entity which operation operate on and this need to be of IEntity</typeparam>
   public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T:class, IEntity
    {
        protected readonly ApplicationDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public GenericRepositoryAsync(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbSet = _dbcontext.Set<T>();
        }

        public async Task<T> AddAsync(T entity) => (await _dbSet.AddAsync(entity)).Entity;
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            return true;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            return true;
        }
        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }
        public async Task<T> FindByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public Task SaveAsync() => _dbcontext.SaveChangesAsync();
        public async Task<T> UpdateAsync(T entity) 
        {
            // let's trick it  - delete the current Id and add the new .
            var findEntity = await FindByIdAsync(entity.Id);

            if (findEntity == null)
                return (await Task.FromResult(_dbSet.Update(entity))).Entity;
            else
            {
                _dbSet.Remove(findEntity);
                await SaveAsync();

                var newEntity = await AddAsync(entity);
                return newEntity;
            }

        }
        

    }
}

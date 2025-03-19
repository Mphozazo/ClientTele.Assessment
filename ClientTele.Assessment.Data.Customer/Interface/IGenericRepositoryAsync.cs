using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientTele.Assessment.Data.Customer.Interface
{
    /// <summary>
    /// Repository base interface for async operations
    /// </summary>
    /// <typeparam name="T">Entity which operation work on.</typeparam>
    /// <remarks>This is an Repository and unit of work pattern </remarks>
    public interface IGenericRepositoryAsync<T> where T : class, IEntity
    {

        /// <summary>
        /// Get entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Find entity by condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="actionUser"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Delete entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actionUser"></param>
        /// <returns>True if succeded otherwise false</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="actionUser"></param>
        /// <returns>True if succeded otherwise false</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="actionUser"></param>
        /// <returns>Updated Entity</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}

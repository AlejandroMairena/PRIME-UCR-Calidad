using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories
{
    // generic repository with basic CRUD operations
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<T> GetByKey(TKey key);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> Insert(TKey key, T model);
        Task Delete(TKey key);
        Task Update(TKey key, T model);
    }
}

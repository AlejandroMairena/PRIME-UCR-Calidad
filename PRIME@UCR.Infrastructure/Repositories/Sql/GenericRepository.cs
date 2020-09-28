using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        protected readonly ISqlDataProvider _db;

        public GenericRepository(ISqlDataProvider dataProvider)
        {
            _db = dataProvider;
        }

        public async Task DeleteAsync(TKey key)
        {
            var existing = await _db.Set<T>().FindAsync(key);
            if (existing != null)
            {
                _db.Set<T>().Remove(existing);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetByKeyAsync(TKey key)
        {
            return await _db.Set<T>().FindAsync(key);
        }

        public async Task<T> InsertAsync(T model)
        {
            _db.Set<T>().Add(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task UpdateAsync(T model)
        {
            _db.Set<T>().Update(model);
            await _db.SaveChangesAsync();
        }
    }
}

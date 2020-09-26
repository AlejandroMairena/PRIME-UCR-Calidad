using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql
{
    public class SqlGenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        protected readonly ISqlDataProvider _db;

        public SqlGenericRepository(ISqlDataProvider dataProvider)
        {
            _db = dataProvider;
        }

        public async Task Delete(TKey key)
        {
            var existing = await _db.Set<T>().FindAsync(key);
            if (existing != null)
            {
                _db.Set<T>().Remove(existing);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetByKey(TKey key)
        {
            return await _db.Set<T>().FindAsync(key);
        }

        public async Task<T> Insert(TKey key, T model)
        {
            _db.Set<T>().Add(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task Update(TKey key, T model)
        {
            _db.Set<T>().Update(model);
            await _db.SaveChangesAsync();
        }
    }
}

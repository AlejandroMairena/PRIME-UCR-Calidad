using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Infrastructure.DataProviders;
using RepoDb;

namespace PRIME_UCR.Infrastructure.Repositories.Sql
{
    public class RepoDbRepository<T, TKey> : IRepoDbRepository<T, TKey> where T : class
    {
        protected readonly ISqlDataProvider _db;

        protected RepoDbRepository(ISqlDataProvider dataProvider)
        {
            _db = dataProvider;
        }

        public virtual async Task DeleteAsync(TKey key)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                var existing = await GetByKeyAsync(key);

                if (existing == null)
                {
                    throw new ArgumentException($"Invalid key for entity {nameof(T)}: {key}");
                }

                await connection.DeleteAsync(existing);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                return await connection.QueryAllAsync<T>();
            }
        }

        public virtual async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                return await connection.QueryAsync(expression);
            }
        }

        public virtual async Task<T> GetByKeyAsync(TKey key)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                return (await connection.QueryAsync<T>(key)).FirstOrDefault();
            }
        }

        public virtual async Task<T> InsertAsync(T model)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                var key = (TKey)await connection.InsertAsync(model);
                return await GetByKeyAsync(key);
            }
        }

        public virtual async Task UpdateAsync(T model)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                await connection.UpdateAsync(model);
            }
        }
    }
}
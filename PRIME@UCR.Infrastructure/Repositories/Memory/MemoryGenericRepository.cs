using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Memory
{
    // implementation of an IGenericRepository that uses a dictionary to store models in memory
    public class MemoryGenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        protected readonly IMemoryDataProvider<T, TKey> DataProvider;

        public MemoryGenericRepository(IMemoryDataProvider<T, TKey> dataProvider)
        {
            DataProvider = dataProvider;
        }

        public Task Delete(TKey key)
        {
            DataProvider.Data.Remove(key);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return Task.FromResult(
                (IEnumerable<T>)DataProvider.Data.Values
            );
        }

        public Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(DataProvider.Data.Values.Where(expression.Compile()));
        }

        public Task<T> GetByKey(TKey key)
        {
            DataProvider.Data.TryGetValue(key, out var result);
            return Task.FromResult(result);
        }

        public Task<T> Insert(TKey key, T model)
        {
            DataProvider.Data.Add(key, model);
            return Task.FromResult(model);
        }

        public Task Update(TKey key, T model)
        {
            if (DataProvider.Data.ContainsKey(key))
            {
                DataProvider.Data.Remove(key);
                DataProvider.Data.Add(key, model);
            }
            else
            {
                throw new ArgumentException("Key does not exist");
            }
            return Task.CompletedTask;
        }
    }
}

using PRIME_UCR.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRIME_UCR.Infrastructure.Repositories
{
    // implementation of an IGenericRepository that uses a dictionary to store models in memory
    public class InMemoryGenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        protected IDictionary<TKey, T> database;

        public InMemoryGenericRepository()
        {
            database = new Dictionary<TKey, T>();
        }

        public void Delete(TKey key)
        {
            database.Remove(key);
        }

        public IEnumerable<T> GetAll()
        {
            return database.Values;
        }

        public T GetByKey(TKey key)
        {
            T result;
            database.TryGetValue(key, out result);
            return result;
        }

        public T Insert(TKey key, T model)
        {
            database.Add(key, model);
            return model;
        }

        public void Update(TKey key, T model)
        {
            if (database.ContainsKey(key))
            {
                database.Remove(key);
                database.Add(key, model);
            }
            else
            {
                throw new ArgumentException("Key does not exist");
            }
        }
    }
}

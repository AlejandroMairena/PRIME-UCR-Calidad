using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Repositories
{
    // generic repository with basic CRUD operations
    public interface IGenericRepository<T, TKey> where T : class
    {
        T GetByKey(TKey key);
        IEnumerable<T> GetAll();
        T Insert(TKey key, T model);
        void Delete(TKey key);
        void Update(TKey key, T model);
    }
}

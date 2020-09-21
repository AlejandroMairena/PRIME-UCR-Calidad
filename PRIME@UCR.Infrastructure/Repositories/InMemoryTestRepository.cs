using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRIME_UCR.Infrastructure.Repositories
{
    public class InMemoryTestRepository : InMemoryGenericRepository<TestModel, int>, ITestRepository
    {
        // no need to implement the basic CRUD operations because they are inherited from InMemoryGenericRepository
        public IEnumerable<TestModel> GetByValue(int value)
        {
            // use LINQ to search for models that have a matching value
            return database.Values.Where(t => t.Value == value);
        }
    }
}

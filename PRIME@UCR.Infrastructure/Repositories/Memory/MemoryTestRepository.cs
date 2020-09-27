using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Memory
{
    public class MemoryTestRepository : MemoryGenericRepository<TestModel, int>, ITestRepository
    {
        public MemoryTestRepository(IMemoryDataProvider<TestModel, int> dataProvider) : base(dataProvider)
        {
        }
        
        // no need to implement the basic CRUD operations because they are inherited from InMemoryGenericRepository
        public Task<IEnumerable<TestModel>> GetByValue(int value)
        {
            // use LINQ to search for models that have a matching value
            return Task.FromResult(
                DataProvider.Data.Values.Where(t => t.Value == value)
            );
        }

    }
}

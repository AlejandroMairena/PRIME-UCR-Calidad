using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql
{
    public class SqlTestRepository : SqlGenericRepository<TestModel, int>, ITestRepository
    {
        public SqlTestRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<TestModel>> GetByValue(int value)
        {
            return await this.GetByCondition(testModel => testModel.Value == value);
        }
    }
}

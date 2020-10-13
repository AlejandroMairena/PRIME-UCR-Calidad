using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.CheckList
{
    class SqlCheckListRepository : GenericRepository<CheckList, int>, ICheckListRepository
    {
        public SqlCheckListRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<CheckList>> GetByName(string name)
        {
            return await this.GetByConditionAsync(checkListModel => checkListModel.Nombre == name);
        }
    }
}

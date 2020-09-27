using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories
{
    public interface ICheckListRepository : IGenericRepository<CheckList, int>
    {
        Task<IEnumerable<CheckList>> GetByName(string name);
    }
}

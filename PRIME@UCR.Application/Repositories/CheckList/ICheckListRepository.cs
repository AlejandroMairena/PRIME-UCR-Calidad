using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.CheckList
{
    public interface ICheckListRepository : IGenericRepository<PRIME_UCR.Domain.Models.CheckList, int>
    {
        Task<IEnumerable<PRIME_UCR.Domain.Models.CheckList>> GetByName(string name);
    }
}

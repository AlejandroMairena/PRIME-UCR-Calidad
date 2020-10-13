using PRIME_UCR.Domain.Models.CheckList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.CheckList
{
    class IItemRepository
    {
        Task<IEnumerable<Item>> GetByName(string name);
    }
}

﻿using PRIME_UCR.Domain.Models.CheckLists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.CheckLists
{
    public interface IItemRepository : IGenericRepository<Item, int>
    {
        Task<IEnumerable<Item>> GetByName(string name);
    }
}

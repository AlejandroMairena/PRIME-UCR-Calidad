﻿using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories
{
    public interface ITestRepository : IGenericRepository<TestModel, int>
    {
        // no need to add basic CRUD operations since they are inherited from IGenericRepository
        Task<IEnumerable<TestModel>> GetByValue(int value);
    }
}

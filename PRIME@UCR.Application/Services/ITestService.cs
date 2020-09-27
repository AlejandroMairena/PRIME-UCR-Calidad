using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services
{
    public interface ITestService
    {
        Task<TestModel> InsertRandomModel();
        Task<IEnumerable<TestModel>> GetAll();
    }
}

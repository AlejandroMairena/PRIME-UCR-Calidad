using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _repo;

        // uses Dependency Injection (DI)
        public TestService(ITestRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TestModel>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<TestModel> InsertRandomModel()
        {
            // business logic goes here
            Random rand = new Random();
            TestModel model = new TestModel { Value = rand.Next() };
            // calls infrastructure to access data, but doesn't care about the implementation for this
            return await _repo.Insert(model.Key, model);
        }
    }
}

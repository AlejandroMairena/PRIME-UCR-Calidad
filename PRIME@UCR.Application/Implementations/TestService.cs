using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IEnumerable<TestModel> GetAll()
        {
            return _repo.GetAll();
        }

        public TestModel InsertRandomModel()
        {
            // business logic goes here
            Random rand = new Random();
            TestModel model = new TestModel { Key = rand.Next(), Value = rand.Next() };
            // calls infrastructure to access data, but doesn't care about the implementation for this
            return _repo.Insert(model.Key, model);
        }
    }
}

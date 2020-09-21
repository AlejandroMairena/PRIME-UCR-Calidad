using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Services
{
    public interface ITestService
    {
        TestModel InsertRandomModel();
        IEnumerable<TestModel> GetAll();
    }
}

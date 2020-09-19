using PRIME_UCR.BusinessLogic.Interfaces;
using PRIME_UCR.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.BusinessLogic.Services
{
    public class TestService : ITestService
    {
        public TestModel CreateModel(int value)
        {
            return new TestModel { MyProperty = value };
        }
    }
}

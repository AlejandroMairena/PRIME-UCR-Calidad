using PRIME_UCR.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.BusinessLogic.Interfaces
{
    public interface ITestService
    {
        TestModel CreateModel(int value);
    }
}

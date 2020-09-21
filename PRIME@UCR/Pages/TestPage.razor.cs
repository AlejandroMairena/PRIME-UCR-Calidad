using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages
{
    public partial class TestPage
    {
        private IEnumerable<TestModel> models;

        // dependency injection
        [Inject] public ITestService MyService { get; set; }

        protected override void OnInitialized()
        {
            // UI logic goes here
            models = MyService.GetAll();
        }

        private void AddRandomModel()
        {
            // UI logic goes here
            // never write business logic here, always call a service from the Application layer for that
            MyService.InsertRandomModel();
        }
    }
}

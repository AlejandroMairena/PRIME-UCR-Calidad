using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages
{
    public class TestPageBase : ComponentBase
    {
        protected IEnumerable<TestModel> models;

        // dependency injection
        [Inject] protected ITestService MyService { get; set; }

        private async Task RefreshModels()
        {
            models = await MyService.GetAll();
        }

        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        protected async Task AddRandomModel()
        {
            // UI logic goes here
            // never write business logic here, always call a service from the Application layer for that
            await MyService.InsertRandomModel();
            await RefreshModels();
        }
    }
}

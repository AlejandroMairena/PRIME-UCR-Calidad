using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Services.CheckLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.IntegrationTests.CheckLists
{
    public class InstanceCheckListServiceTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public InstanceCheckListServiceTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetByIncidentCodeReturnsEmpty()
        {
            /* Case: There are no instance checklists in post deployment
             * -> returns an empty list because there is no instance checklist with this code
             */
            var checkListService = _factory.Services.GetRequiredService<IInstanceChecklistService>();
            string incidentCode = "Pruebas";
            var result = await checkListService.GetByIncidentCod(incidentCode);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetItemsByIncidentCodAndCheckListIdReturnsEmpty()
        {
            /* Case: There are no instance checklists in post deployment
             * -> returns an empty list because there is no instance checklist with this code or with this id.
             */
            var checkListService = _factory.Services.GetRequiredService<IInstanceChecklistService>();
            string incidentCode = "Pruebas";
            var result = await checkListService.GetItemsByIncidentCodAndCheckListId(incidentCode, 1);
            Assert.Empty(result);
        }
    }
}

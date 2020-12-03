using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Services.CheckLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.IntegrationTests.CheckLists
{
    public class CheckListServiceIntegrationTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public CheckListServiceIntegrationTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task GetAllReturnsNotEmpty()
        {
            /* Case: There are checklists in post deployment
             * -> the list of all checklists wont be empty.
             */
            var checkListService = _factory.Services.GetRequiredService<ICheckListService>();
            var result = await checkListService.GetAll();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCheckListReturnsValid()
        {
            /* Case: There are checklists in post deployment
             * -> returns a checklist because the id of the list exists in the post deployment
             */
            var checkListService = _factory.Services.GetRequiredService<ICheckListService>();
            var result = await checkListService.GetById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCheckListReturnsNull()
        {
            /* Case: There are checklists in post deployment
             * -> returns null because the id of the list does not exist
             */
            var checkListService = _factory.Services.GetRequiredService<ICheckListService>();
            var result = await checkListService.GetById(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCoreItemsReturnsValid()
        {
            /* Case: There are checklists in post deployment
             * -> returns a IEnumerable<Item> because the id of the list exists in the post deployment and that list has items.
             */
            var checkListService = _factory.Services.GetRequiredService<ICheckListService>();
            var result = await checkListService.GetCoreItems(3);
            Assert.Equal(5, result.Count());
        }
    }
}

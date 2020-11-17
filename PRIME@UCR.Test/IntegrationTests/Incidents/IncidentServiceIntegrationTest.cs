using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Services.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.IntegrationTests.Incidents
{
    public class IncidentServiceIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public IncidentServiceIntegrationTest(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllIncidentsReturnsNotEmpty() 
        {
            /* Case: There are incidents in post deployment
             * -> the list of all incidents wont be empty.
             */
            var incidentService = _factory.Services.GetRequiredService<IIncidentService>();
            var result = await incidentService.GetAllAsync();
            Assert.NotEmpty(result);
        }


    }
}

using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Services.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.IntegrationTests.Incidents
{
    public class AssignementServiceIntegrationTest: IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public AssignementServiceIntegrationTest(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllTransportUnitsByModeReturnsEmpty()
        {

            var assignementService = _factory.Services.GetRequiredService<IAssignmentService>();
            var result = await assignementService.GetAllTransportUnitsByMode(null);
            Assert.Empty(result);
        } 

        [Fact]
        public async Task GetAllTransportUnitsByModeReturnsValid()
        {

            var assignementService = _factory.Services.GetRequiredService<IAssignmentService>();
            var result = await assignementService.GetAllTransportUnitsByMode("Terrestre");
            Assert.Collection(result,
                                  unidad => Assert.Equal("BPC086", unidad.Matricula),
                                  unidad => Assert.Equal("FMM420", unidad.Matricula)
                                  );
        } 

        [Fact]
        public async Task GetAllTransportUnitsByModeReturnsInvalid()
        {

            var assignementService = _factory.Services.GetRequiredService<IAssignmentService>();
            var result = await assignementService.GetAllTransportUnitsByMode("Invalid unit");
            Assert.Null(result);
        } 

    }
}

using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Services.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.IntegrationTests.Multimedia
{
    public class MultimediaContentServiceIntegrationTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;
        private readonly IMultimediaContentService mcService;

        public MultimediaContentServiceIntegrationTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            mcService = _factory.Services.GetRequiredService<IMultimediaContentService>();
        }

        [Fact]
        public async Task GetByIdReturnsNull()
        {
            var result = await mcService.GetById(-1);
            Assert.Null(result);
        }

    }
}

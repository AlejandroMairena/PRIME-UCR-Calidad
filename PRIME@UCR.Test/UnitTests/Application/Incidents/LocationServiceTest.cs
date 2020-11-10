using Moq;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.UnitTests.Application.Incidents
{
    public class LocationServiceTest
    {
        [Fact]
        public async void GetCountryByNameReturnsNull()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(p => p.GetByKeyAsync(String.Empty)).Returns(Task.FromResult<Pais>(null));
            var locationService = new LocationService(mockRepo.Object, null, null, null, null);
            var result = await locationService.GetCountryByName(String.Empty);
            Assert.Null(result);
        }
    }
}

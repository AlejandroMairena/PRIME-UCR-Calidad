using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using Xunit;

namespace PRIME_UCR.Test.UnitTests.Application.Incidents
{
    public class IncidentServiceTests
    {
        [Fact]
        public async Task GetAllAsyncReturnsEmptyList()
        {
            // arrange
            var mockRepo = new Mock<IIncidentRepository>();
            var data = new List<Incidente>();

            mockRepo
                .Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Incidente>>(data));

            var service = new IncidentService(
                mockRepo.Object,
                null, null, null, null, null, null);

            // act 
            var result = await service.GetAllAsync();

            // assert
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task GetAllAsyncReturnsValidList()
        {
            // arrange
            var mockRepo = new Mock<IIncidentRepository>();
            var data = new List<Incidente>
            {
                new Incidente {Codigo = "codigo1"},
                new Incidente {Codigo = "codigo2"},
                new Incidente {Codigo = "codigo3"},
                new Incidente {Codigo = "codigo4"},
                new Incidente {Codigo = "codigo5"},
                new Incidente {Codigo = "codigo6"},
            };

            mockRepo
                .Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Incidente>>(data));

            var service = new IncidentService(
                mockRepo.Object,
                null, null, null, null, null, null);

            // act 
            var result = await service.GetAllAsync();

            // assert
            Assert.Collection(result,
                                  incidente => Assert.Equal("codigo1", incidente.Codigo),
                                  incidente => Assert.Equal("codigo2", incidente.Codigo),
                                  incidente => Assert.Equal("codigo3", incidente.Codigo),
                                  incidente => Assert.Equal("codigo4", incidente.Codigo),
                                  incidente => Assert.Equal("codigo5", incidente.Codigo),
                                  incidente => Assert.Equal("codigo6", incidente.Codigo)
                              );
        }
        
        [Fact]
        public async Task GetIncidentAsyncReturnsNull()
        {
            // arrange
            var mockRepo = new Mock<IIncidentRepository>();
            Incidente data = null;

            mockRepo
                .Setup(p => p.GetByKeyAsync("código inválido"))
                .Returns(Task.FromResult(data));

            var service = new IncidentService(
                mockRepo.Object,
                null, null, null, null, null, null);

            // act 
            var result = await service.GetIncidentAsync("código inválido");

            // assert
            Assert.Null(result);
        }
        
        [Fact]
        public async Task GetIncidentAsyncReturnsValid()
        {
            // arrange
            var mockRepo = new Mock<IIncidentRepository>();
            var data = new Incidente{ Codigo = "código válido"};

            mockRepo
                .Setup(p => p.GetByKeyAsync("código válido"))
                .Returns(Task.FromResult(data));

            var service = new IncidentService(
                mockRepo.Object,
                null, null, null, null, null, null);

            // act 
            var result = await service.GetIncidentAsync("código válido");

            // assert
            Assert.NotNull(result);
            Assert.Equal("código válido", result.Codigo);
        }
    }
}
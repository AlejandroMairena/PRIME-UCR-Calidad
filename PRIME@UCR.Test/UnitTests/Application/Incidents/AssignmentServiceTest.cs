using Moq;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using NuGet.Frameworks;
using PRIME_UCR.Domain.Models;
using Castle.DynamicProxy.Generators;

namespace PRIME_UCR.Test.UnitTests.Application.Incidents
{
    public class AssignmentServiceTest
    {
        [Fact]
        public async Task GetCoordinatorsAsyncReturnsEmpty()
        {
            /*If there are no coordinators registered, the service should return an empty array. 
             */
            var mockRepo = new Mock<ICoordinadorTécnicoMédicoRepository>();
            mockRepo
                .Setup(p => 
                    p.GetAllAsync()).
                        Returns(Task.FromResult<IEnumerable<CoordinadorTécnicoMédico>>(new List<CoordinadorTécnicoMédico>())
                );
            var AssignmentService = new AssignmentService(null, mockRepo.Object, null, null, null);
            var result = await AssignmentService.GetCoordinatorsAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCoordinatorsAsyncReturnsQuantity()
        {
            /*If there are coordinators registered, the service should not return an empty array. 
             */
            var mockRepo = new Mock<ICoordinadorTécnicoMédicoRepository>();
            List<CoordinadorTécnicoMédico> MyList = new List<CoordinadorTécnicoMédico>
            {
                new CoordinadorTécnicoMédico(),
                new CoordinadorTécnicoMédico(),
                new CoordinadorTécnicoMédico(),
                new CoordinadorTécnicoMédico(),
            };
            mockRepo
                .Setup(p =>
                    p.GetAllAsync())
                        .Returns(Task.FromResult<IEnumerable<CoordinadorTécnicoMédico>>(MyList)
                );
            var AssignmentService = new AssignmentService(null, mockRepo.Object, null, null, null);
            var result = await AssignmentService.GetCoordinatorsAsync();
            Assert.NotEmpty(result);
            Assert.Equal(MyList, result.ToList());
        }

        [Fact]
        public async Task AssignToIncidentAsyncRuns()
        {
            /*If the service receives valid entries it should run flawlessly. 
             */
            var IncidentRepo = new Mock<IIncidentRepository>();
            var AssignmentRepo = new Mock<IAssignemntRepository>();
            string ParameterCode = "TestValue";
            Incidente IncidentToReturn = new Incidente();
            
            IncidentRepo
                .Setup(p => 
                    p.GetByKeyAsync(ParameterCode))
                        .Returns(Task.FromResult<Incidente>(IncidentToReturn));
            
            UnidadDeTransporte TransportUnitToTest = new UnidadDeTransporte
            {
                Matricula = "1234567"
            };
            CoordinadorTécnicoMédico AssignedCoordinatorToTest = new CoordinadorTécnicoMédico
            {
                Cédula = "1234567"
            };
            AssignmentModel ParameterModel = new AssignmentModel 
            { 
                Coordinator = AssignedCoordinatorToTest,
                TransportUnit = TransportUnitToTest            
            };
            var AssignmentService = new AssignmentService(null, null, null, AssignmentRepo.Object, IncidentRepo.Object);
            await AssignmentService.AssignToIncidentAsync(ParameterCode, ParameterModel);
        }
    }
}
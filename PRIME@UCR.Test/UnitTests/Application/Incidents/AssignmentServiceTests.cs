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
using Microsoft.AspNetCore.DataProtection;

namespace PRIME_UCR.Test.UnitTests.Application.Incidents
{
    public class AssignmentServiceTest
    {
        [Fact]
        public async Task GetAllTransportUnitsByModeReturnsEmpty()
        {
            var mockRepo = new Mock<ITransportUnitRepository>();
            mockRepo.Setup(p => p.GetAllTransporUnitsByMode(String.Empty))
                .Returns(Task.FromResult<IEnumerable<UnidadDeTransporte>>(new List<UnidadDeTransporte>()));
            var assignmentService = new AssignmentService(mockRepo.Object, null, null, null, null);
            var result = await assignmentService.GetAllTransportUnitsByMode(String.Empty);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllTransportUnitsByModeReturnsQuantity()
        {
            /*If there are coordinators registered, the service should not return an empty array.
             */
            var mockRepo = new Mock<ITransportUnitRepository>();
            List<UnidadDeTransporte> MyList = new List<UnidadDeTransporte>
            {
                new UnidadDeTransporte(),
                new UnidadDeTransporte(),
                new UnidadDeTransporte(),
                new UnidadDeTransporte(),
            };
            mockRepo
                .Setup(p =>
                    p.GetAllTransporUnitsByMode("Accion"))
                        .Returns(Task.FromResult<IEnumerable<UnidadDeTransporte>>(MyList)
                );
            var assignmentService = new AssignmentService(mockRepo.Object, null, null, null, null);
            var result = await assignmentService.GetAllTransportUnitsByMode("Accion");
            Assert.NotEmpty(result);
            Assert.Equal(MyList, result.ToList());

        }

        [Fact]
        public async Task GetAssignmentsByIncidentIdAsyncReturnsValid()
        {
            var mockRepo = new Mock<IIncidentRepository>();
            var mockRepo1 = new Mock<ICoordinadorTécnicoMédicoRepository>();
            var mockRepo2 = new Mock<ITransportUnitRepository>();
            var mockRepo3 = new Mock<IAssignmentRepository>();
            var incident = new Incidente
            {
                Codigo = "12",
                CedulaTecnicoCoordinador = "11111111",
                MatriculaTrans = "XYX"
            };
            var coordinator = new CoordinadorTécnicoMédico {};
            var TUnit = new UnidadDeTransporte
            {
                Matricula = "XYX"
            };
            var specialist = new List<EspecialistaTécnicoMédico>
            {
                new EspecialistaTécnicoMédico()
            };
            mockRepo.Setup(p => p.GetByKeyAsync(incident.Codigo))
                .Returns(Task.FromResult<Incidente>(incident));
            mockRepo1.Setup(p => p.GetByKeyAsync(incident.CedulaTecnicoCoordinador))
                .Returns(Task.FromResult<CoordinadorTécnicoMédico>(coordinator));
            mockRepo2.Setup(p => p.GetByKeyAsync(incident.MatriculaTrans))
                .Returns(Task.FromResult<UnidadDeTransporte>(TUnit));
            mockRepo3.Setup(p => p.GetAssignmentsByIncidentIdAsync(incident.Codigo))
                .Returns(Task.FromResult<IEnumerable<EspecialistaTécnicoMédico>>(specialist));
            var assignmentService = new AssignmentService(mockRepo2.Object, mockRepo1.Object, null, mockRepo3.Object, mockRepo.Object);
            var result = await assignmentService.GetAssignmentsByIncidentIdAsync(incident.Codigo);
            Assert.Equal(incident.MatriculaTrans, result.TransportUnit.Matricula);
        }

        [Fact]
        public async Task GetAssignmentsByIncidentIdAsyncThrowsNullException()
        {
            var mockRepo = new Mock<IIncidentRepository>();
            mockRepo.Setup(p => p.GetByKeyAsync(String.Empty))
                .Returns(Task.FromResult<Incidente>(null));
            var assignmentService = new AssignmentService(null, null, null, null, mockRepo.Object);
            await Assert.ThrowsAsync<ArgumentException>(() => assignmentService.GetAssignmentsByIncidentIdAsync(String.Empty));
        }

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
            var AssignmentRepo = new Mock<IAssignmentRepository>();
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

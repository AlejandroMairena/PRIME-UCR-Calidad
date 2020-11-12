using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;
using Xunit;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models.MedicalRecords;
using System;

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

        [Fact]
        public async Task GetIncidentDetailsAsyncReturnsValid()
        {
            /*If the service receives valid entries it should run flawlessly. 
             */
            var _MockIncidentRepository = new Mock<IIncidentRepository>(); 
            var _MockTransportUnitRepository = new Mock<ITransportUnitRepository>();
            var _MockPersonRepository = new Mock<IPersonaRepository>(); 
            var _MockStateRepository = new Mock<IIncidentStateRepository>(); 
            string CodeToTest = "codigoTotest";
            Cita CitaToTest = new Cita
            {
                Expediente = new Expediente { CedulaPaciente = "123", CedulaMedicoDuenno = "1234" },
                FechaHoraCreacion = DateTime.Today,
                FechaHoraEstimada = DateTime.Today,
            };
            Estado StateToTest = new Estado
            {
                Nombre = "estadoValido"
            };
            Incidente IncidentToTest = new Incidente
            {
                Codigo = CodeToTest,
                Cita = CitaToTest,
                CedulaRevisor = "cedulaValida",
                Modalidad = "modalityToTest", 
                CedulaAdmin = "cedulaValida", 
                CodigoCita = 1
            };
            UnidadDeTransporte TransportUnitToTest = new UnidadDeTransporte
            {
                Matricula = "validString"
            };
            Persona ReviewerToTest = new Persona
            {
                Cédula = "cedulaValida"
            };
            _MockIncidentRepository
                .Setup(p => p.GetWithDetailsAsync(CodeToTest))
                .Returns(Task.FromResult(IncidentToTest));
            _MockTransportUnitRepository
                .Setup(p => p.GetTransporUnitByIncidentIdAsync(IncidentToTest.Codigo))
                .Returns(Task.FromResult(TransportUnitToTest));
            _MockPersonRepository
                .Setup(p => p.GetByKeyPersonaAsync(IncidentToTest.CedulaRevisor))
                .Returns(Task.FromResult(ReviewerToTest));
            _MockStateRepository
                .Setup(p => p.GetCurrentStateByIncidentId(IncidentToTest.Codigo))
                .Returns(Task.FromResult(StateToTest));
            var incidentServiceToTest = new IncidentService(_MockIncidentRepository.Object, null, _MockStateRepository.Object, null, _MockTransportUnitRepository.Object, null, _MockPersonRepository.Object);

            IncidentDetailsModel result =  await incidentServiceToTest.GetIncidentDetailsAsync(CodeToTest);
            Assert.True
                (
                    result.Code == IncidentToTest.Codigo
                    && result.Mode == IncidentToTest.Modalidad
                    && result.CurrentState == StateToTest.Nombre
                    && result.RegistrationDate == CitaToTest.FechaHoraCreacion
                    && result.EstimatedDateOfTransfer == CitaToTest.FechaHoraEstimada
                    && result.AdminId == IncidentToTest.CedulaAdmin
                    && result.AppointmentId == IncidentToTest.CodigoCita
                    && result.TransportUnitId == TransportUnitToTest.Matricula
                    && result.MedicalRecord == CitaToTest.Expediente
                    && result.Reviewer.Cédula == ReviewerToTest.Cédula
                );
        }

        [Fact]
        public async Task RejectIncidentAsyncReturnsValid() 
        {
            /*If the service receives valid entries it should run flawlessly. 
             */
            var _MockIncidentRepository = new Mock<IIncidentRepository>();
            var _MockStateRepository = new Mock<IIncidentStateRepository>();
            string code = "CodeToTest";
            string reviewerId = "ReviewerIDToTest";
            Incidente IncidentToTest = new Incidente
            {
                Codigo = code,
                CedulaRevisor = "cedulaValida",
                Modalidad = "modalityToTest",
                CedulaAdmin = "cedulaValida",
                CodigoCita = 1
            };
            Estado StateToTest = new Estado
            {
                Nombre = "Creado"
            };
            _MockIncidentRepository
                .Setup(p => p.GetByKeyAsync(code))
                .Returns(Task.FromResult(IncidentToTest));
            _MockStateRepository
                .Setup(p => p.GetCurrentStateByIncidentId(code))
                .Returns(Task.FromResult(StateToTest));
            var incidentServiceToTest = new IncidentService(_MockIncidentRepository.Object, null, _MockStateRepository.Object, null, null, null, null);

            await incidentServiceToTest.RejectIncidentAsync(code, reviewerId);
        }



    }
}
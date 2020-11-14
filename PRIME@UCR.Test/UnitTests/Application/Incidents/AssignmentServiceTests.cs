﻿using Moq;
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

        [Fact]
        public async Task GetSpecialistsAsyncReturnsEmpty()
        {
            /*There are no Specialists test case -> returns empty list*/
            var mockRepo = new Mock<IEspecialistaTécnicoMédicoRepository>();
            mockRepo
                .Setup(p =>
                    p.GetAllAsync()).
                        Returns(Task.FromResult<IEnumerable<EspecialistaTécnicoMédico>>(new List<EspecialistaTécnicoMédico>())
                );
            var AssignmentService = new AssignmentService(null, null, mockRepo.Object, null, null);
            var result = await AssignmentService.GetSpecialistsAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetSpecialistsAsyncReturnsQuantity()
        {
            /* Positive test case, will check the amount of Specialists returned,
             * with the amount of Specialists that exists.
            */
            var mockRepo = new Mock<IEspecialistaTécnicoMédicoRepository>();
            List<EspecialistaTécnicoMédico> MyList = new List<EspecialistaTécnicoMédico>
            {
                new EspecialistaTécnicoMédico(),
                new EspecialistaTécnicoMédico(),
                new EspecialistaTécnicoMédico(),
            };
            mockRepo
                .Setup(p =>
                    p.GetAllAsync())
                        .Returns(Task.FromResult<IEnumerable<EspecialistaTécnicoMédico>>(MyList)
                );
            var AssignmentService = new AssignmentService(null, null, mockRepo.Object, null, null);
            var result = await AssignmentService.GetSpecialistsAsync();
            Assert.NotEmpty(result);
            Assert.Equal(MyList, result.ToList());
        }

        [Fact]
        public async Task IsAuthorizedToViewPatientWithCoordinatorReturnsTrue()
        {
            /* Case: There is a coordinator linked with incident. The coordinator is
             * the one who is checking the patient info.
             * -> Returns true because the coordinator is authorized
            */
            var IncidentRepo = new Mock<IIncidentRepository>();
            var CoordinatorRepo = new Mock<ICoordinadorTécnicoMédicoRepository>();
            var TransportRepo = new Mock<ITransportUnitRepository>();
            var AssignmentRepo = new Mock<IAssignmentRepository>();
            //Coordinator
            var coordinator = new CoordinadorTécnicoMédico { Cédula = "987654321" };
            var incident = new Incidente
            {
                Codigo = "0101",
                MatriculaTrans = "LOL",
                CedulaTecnicoCoordinador = coordinator.Cédula,
            };
            var TUnit = new UnidadDeTransporte
            {
                Matricula = "LOL"
            };
            var specialist = new List<EspecialistaTécnicoMédico>
            {
                new EspecialistaTécnicoMédico()
            };
            // Repositories setup
            IncidentRepo.Setup(p => p.GetByKeyAsync(incident.Codigo))
                .Returns(Task.FromResult<Incidente>(incident));
            IncidentRepo.Setup(p => p.GetAssignedOriginDoctor(String.Empty))
                .Returns(Task.FromResult<Médico>(null));
            IncidentRepo.Setup(p => p.GetAssignedDestinationDoctor(String.Empty))
                .Returns(Task.FromResult<Médico>(null));
            CoordinatorRepo.Setup(p => p.GetByKeyAsync(incident.CedulaTecnicoCoordinador))
                .Returns(Task.FromResult<CoordinadorTécnicoMédico>(coordinator));
            TransportRepo.Setup(p => p.GetByKeyAsync(incident.MatriculaTrans))
                .Returns(Task.FromResult<UnidadDeTransporte>(TUnit));
            AssignmentRepo.Setup(p => p.GetAssignmentsByIncidentIdAsync(incident.Codigo))
                .Returns(Task.FromResult<IEnumerable<EspecialistaTécnicoMédico>>(specialist));

            // call to service
            var AssignmentService = new AssignmentService(TransportRepo.Object, CoordinatorRepo.Object, null, AssignmentRepo.Object, IncidentRepo.Object);
            var result = await AssignmentService.IsAuthorizedToViewPatient(incident.Codigo, incident.CedulaTecnicoCoordinador);
            Assert.True(result);
        }

        [Fact]
        public async Task GetAssignedDestinationDoctorReturnsValid()
        {
            // arrange
            var _MockIncidentRepository = new Mock<IIncidentRepository>();
            var incident1 = new IncidentListModel { Codigo = "123abc" };
            var incident2 = new IncidentListModel { Codigo = "456def" };
            var incident3 = new IncidentListModel { Codigo = "789ghi" };
            string code = "código válido";
            Médico expected = new Médico { Cédula = "122873402" };

            _MockIncidentRepository
                .Setup(p => p.GetAssignedDestinationDoctor(code))
                .Returns(Task.FromResult(expected));


            var AssignmentService = new AssignmentService(null, null, null, null, _MockIncidentRepository.Object);


            // act
            var result = (await AssignmentService.GetAssignedDestinationDoctor(code));
            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetAssignedDestinationDoctorReturnsNull()
        {
            // arrange
            var _MockIncidentRepository = new Mock<IIncidentRepository>();
            var incident1 = new IncidentListModel { Codigo = "123abc" };
            var incident2 = new IncidentListModel { Codigo = "456def" };
            var incident3 = new IncidentListModel { Codigo = "789ghi" };
            string code = "código válido";
            Médico expected = null;

            _MockIncidentRepository
                .Setup(p => p.GetAssignedDestinationDoctor(code))
                .Returns(Task.FromResult(expected));


            var AssignmentService = new AssignmentService(null, null, null, null, _MockIncidentRepository.Object);


            // act
            var result = (await AssignmentService.GetAssignedDestinationDoctor(code));
            // assert
            Assert.Equal(expected, result);
        }
    }
}

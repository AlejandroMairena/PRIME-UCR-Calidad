using Moq;
using PRIME_UCR.Application.Implementations.MedicalRecords;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PRIME_UCR.Domain.Models.UserAdministration; 

namespace PRIME_UCR.Test.UnitTests.Application.MedicalRecords
{
    public class MedicalRecordServiceTest
    {

        [Fact]

        public async void getMedicalRecordByNotValidId() {

            var mockRepo = new Mock<IMedicalRecordRepository>();
            var mockRepoList = new Mock<IMedicalRecordRepository>();
            mockRepo.Setup(p => p.GetDetailsRecordWithPatientDoctorDatesAsync(-1)).Returns(Task.FromResult<Expediente>(null));
            var MedRecordService = new MedicalRecordService(mockRepo.Object, null, null, null, null, null, null, null, null, null, null, null);
            var result = await MedRecordService.GetMedicalRecordDetailsLinkedAsync(-1);
            Assert.Null(result);

        }


        [Fact]
        public async void GetMedicalCenterByNotValidUbicationCenterId() {
            var mockRepo = new Mock<IMedicalRecordRepository>();
            mockRepo.Setup(p => p.GetByKeyAsync(-1)).Returns(Task.FromResult<Expediente>(null));
            var MedRecordService = new MedicalRecordService(mockRepo.Object, null, null, null, null, null, null, null, null, null, null, null);
            var result = await MedRecordService.GetMedicalCenterByUbicationCenterIdAsync(-1);
            Assert.Null(result);

        }


        [Fact]
        public async void getMedicalRecordByValidIdRecord() {

            var mockRepo = new Mock<IMedicalRecordRepository>();
            Expediente record = new Expediente()
            {
                CedulaPaciente = "123456789",
                CedulaMedicoDuenno = "222222222",
                FechaCreacion = DateTime.Now,
                Clinica = "Mexico", 
                Paciente = null,
                Medico = null,
                Alergias = null,
                Antecedentes = null,
                PadecimientosCronicos = null,
                Citas = null
            };

            mockRepo.Setup(p => p.GetByPatientIdAsync("123456789")).Returns(Task.FromResult(record));
            var MedRecordService = new MedicalRecordService(mockRepo.Object, null, null, null, null, null, null, null, null, null, null, null);
            var result = await MedRecordService.GetByPatientIdAsync("123456789");
            Assert.Equal(record, result); 
        }

        [Fact]

        public async void getMedicalRecordDetailedByValidIdentification() {

            var mockRepo = new Mock<IMedicalRecordRepository>();
            Expediente record = new Expediente()
            {
                CedulaPaciente = "123456789",
                CedulaMedicoDuenno = "222222222",
                FechaCreacion = DateTime.Now,
                Clinica = "Mexico",
                Paciente = new Paciente() { Cédula = "123456789", Nombre = "Juan", PrimerApellido = "Perez" },
                Medico = new Médico() { Cédula = "222222222", Nombre = "Juan", PrimerApellido = "Guzman"},
                Alergias = null,
                Antecedentes = null,
                PadecimientosCronicos = null,
                Citas = null
            };

            mockRepo.Setup(p => p.GetDetailsRecordWithPatientDoctorDatesAsync(123456789)).Returns(Task.FromResult(record));
            var MedRecordService = new MedicalRecordService(mockRepo.Object, null, null, null, null, null, null, null, null, null, null, null);
            var result = await MedRecordService.GetMedicalRecordDetailsLinkedAsync(123456789);
            Assert.Equal(record, result);
        }


        [Fact]
        public async void updateMedicalRecord() {


            var mockRepo = new Mock<IMedicalRecordRepository>();
            Expediente record = new Expediente()
            {
                CedulaPaciente = "123456789",
                CedulaMedicoDuenno = "222222222",
                FechaCreacion = DateTime.Now,
                Clinica = "Calderon",
                Paciente = new Paciente() { Cédula = "123456789", Nombre = "Juan", PrimerApellido = "Perez" },
                Medico = new Médico() { Cédula = "222222222", Nombre = "Juan", PrimerApellido = "Guzman" },
                Alergias = null,
                Antecedentes = null,
                PadecimientosCronicos = null,
                Citas = null
            };



            Expediente record_updated = new Expediente()
            {
                CedulaPaciente = "123456789",
                CedulaMedicoDuenno = "222222222",
                FechaCreacion = DateTime.Now,
                Clinica = "Calderon",
                Paciente = new Paciente() { Cédula = "123456789", Nombre = "Juan", PrimerApellido = "Perez" },
                Medico = new Médico() { Cédula = "222222222", Nombre = "Juan", PrimerApellido = "Guzman" },
                Alergias = null,
                Antecedentes = null,
                PadecimientosCronicos = null,
                Citas = null
            };

            mockRepo.Setup(p => p.UpdateMedicalRecordAsync(record)).Returns(Task.FromResult(record_updated));
            var MedRecordService = new MedicalRecordService(mockRepo.Object, null, null, null, null, null, null, null, null, null, null, null);
            var result = await MedRecordService.UpdateMedicalRecordAsync(record);
            Assert.Equal(record, result);
        }


        /*
         
                [Fact]
        public async void getAllergyByRecordIdReturnsValidAllergy()
        {
            var mockRepo = new Mock<IAlergyRepository>();
            var mockRepoList = new Mock<IAlergyListRepository>();  
            var allergyTest = new Alergias
            {
                IdExpediente = 1,
                IdListaAlergia = 1,
                FechaCreacion = DateTime.Now
            };
            var AllergyList = new List<Alergias>
            {
                allergyTest
            };
            IEnumerable<Alergias> AllergyEnumerable = AllergyList;
            mockRepo
             .Setup(p => p.GetByConditionAsync(i => i.IdExpediente == 1))
             .Returns(Task.FromResult(AllergyEnumerable));
            IAlergyService AllergyService = new AlergyService(mockRepo.Object, mockRepoList.Object);
            var result = (await mockRepo.Object.GetByConditionAsync(i => i.IdExpediente == 1)).ToList();
            var result2 = (await AllergyService.GetAlergyByRecordId(1)).ToList();
            Assert.Equal(allergyTest.IdListaAlergia, result2.First().IdListaAlergia);
            Assert.Equal(allergyTest.IdExpediente, result2.First().IdExpediente);
        }
         * */

    }
}

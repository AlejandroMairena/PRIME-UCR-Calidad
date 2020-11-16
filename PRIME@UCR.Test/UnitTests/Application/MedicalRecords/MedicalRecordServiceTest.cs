using Moq;
using PRIME_UCR.Application.Implementations.MedicalRecords;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
            Assert.Equal(result, null);

        }

    }
}

using System.Collections.Generic;
using Xunit;
using Moq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Application.Implementations.MedicalRecords;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;
using PRIME_UCR.Application.Services.MedicalRecords;
using System;
using System.Linq;

namespace PRIME_UCR.Test.UnitTests.Application.MedicalRecords
{
    public class ChronicConditionServiceTest
    {
        //[Fact]
        //public async void getChronicConditionByRecordIdNull()
        //{
        //    var mockRepo = new Mock<IChronicConditionRepository>();
        //    var mockRepoList = new Mock<IChronicConditionListRepository>();
        //    mockRepo.Setup(p => p.GetByConditionAsync(i => i.IdExpediente == 0)).Returns(Task.FromResult<IEnumerable<PadecimientosCronicos>>(null));
        //    IChronicConditionService ChronicConditionService = 
        //        new ChronicConditionService(mockRepo.Object, mockRepoList.Object);
        //    var result = await mockRepo.Object.GetByConditionAsync(a => a.IdExpediente == 0);
        //    var result2 = (await ChronicConditionService.GetChronicConditionByRecordId(0));
        //    Assert.Null(result);
        //    Assert.Null(result2);
        //}

        /*[Fact]
        public async void getChronicConditionByRecordIdInvalid()
        {
            var mockRepo = new Mock<IChronicConditionRepository>();
            var mockRepoList = new Mock<IChronicConditionListRepository>();
            //Sets mocks for repositories
            IChronicConditionService ChronicConditionService =
                new ChronicConditionService(mockRepo.Object, mockRepoList.Object);
            //Creates Service for test
            var result = (await ChronicConditionService.GetChronicConditionByRecordId(-1));
            //Asserts the result
            Assert.Empty(result);
        }

        [Fact]
        public async void InsertChronicConditionAsync()
        {
            var mockRepo = new Mock<IChronicConditionRepository>();
            var mockRepoList = new Mock<IChronicConditionListRepository>();
            //Sets mocks for repositories
            var chronicConditionTest = new PadecimientosCronicos
            {
                IdExpediente = 1,
                IdListaPadecimiento = 1,
                FechaCreacion = DateTime.Now
            };
            IChronicConditionService ChronicConditionService =
                new ChronicConditionService(mockRepo.Object, mockRepoList.Object);
            //Creates Service for test
            var result = await ChronicConditionService.InsertChronicConditionAsync(chronicConditionTest);
            //Asserts the result
            Assert.Equal(chronicConditionTest.IdListaPadecimiento, result.IdListaPadecimiento);
            Assert.Equal(chronicConditionTest.IdExpediente, result.IdExpediente);
        }

        [Fact]
        public async void GetAllAsyncNull()
        {
            var mockRepo = new Mock<IChronicConditionRepository>();
            var mockRepoList = new Mock<IChronicConditionListRepository>();
            //Sets mocks for repositories
            mockRepoList.Setup(p => p.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ListaPadecimiento>>(null));
            IChronicConditionService ChronicConditionService =
                new ChronicConditionService(mockRepo.Object, mockRepoList.Object);
            //Creates Service for test
            var result = await ChronicConditionService.GetAll();
            //Asserts the result
            Assert.Null(result);
        }*/
    }
}


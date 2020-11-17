using System.Collections.Generic;
using Xunit;
using Moq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Application.Implementations.MedicalRecords;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Services.MedicalRecords;

namespace PRIME_UCR.Test.UnitTests.Application.MedicalRecords
{
    public class AllergyServiceTest
    {

        //[Fact]
        //public async void getAllergyByRecordIdNull()
        //{
        //    var mockRepo = new Mock<IAlergyRepository>();
        //    var mockRepoList = new Mock<IAlergyListRepository>();
        //    mockRepo.Setup(p => p.GetByConditionAsync(i => i.IdExpediente == 0)).Returns(Task.FromResult<IEnumerable<Alergias>>(null));
        //    IAlergyService AllergyService = new AlergyService(mockRepo.Object,mockRepoList.Object);
        //    var result = await mockRepo.Object.GetByConditionAsync(a => a.IdExpediente == 0);
        //    var result2 = (await AllergyService.GetAlergyByRecordId(0));
        //    Assert.Null(result);
        //    Assert.Null(result2);
        //}
        [Fact]
        public async void getAllergyByRecordIdInvalid()
        {
            var mockRepo = new Mock<IAlergyRepository>();
            var mockRepoList = new Mock<IAlergyListRepository>();
            IAlergyService AllergyService = new AlergyService(mockRepo.Object, mockRepoList.Object);
            //var result = await mockRepo.Object.GetByConditionAsync(a => a.IdExpediente == 0);
            var result = (await AllergyService.GetAlergyByRecordId(-1));
            Assert.Empty(result);
            //Assert.Null(result2);
        }

        //[Fact]
        //public async void getAllergyByRecordIdReturnsValidAllergy()
        //{
        //    var mockRepo = new Mock<IAlergyRepository>();
        //    var mockRepoList = new Mock<IAlergyListRepository>();  
        //    var allergyTest = new Alergias
        //    {
        //        IdExpediente = 1,
        //        IdListaAlergia = 1,
        //        FechaCreacion = DateTime.Now
        //    };
        //    var AllergyList = new List<Alergias>
        //    {
        //        allergyTest
        //    };
        //    IEnumerable<Alergias> AllergyEnumerable = AllergyList;
        //    mockRepo
        //     .Setup(p => p.GetByConditionAsync(i => i.IdExpediente == 1))
        //     .Returns(Task.FromResult(AllergyEnumerable));
        //    IAlergyService AllergyService = new AlergyService(mockRepo.Object, mockRepoList.Object);
        //    var result = (await mockRepo.Object.GetByConditionAsync(i => i.IdExpediente == 1)).ToList();
        //    var result2 = (await AllergyService.GetAlergyByRecordId(1)).ToList();
        //    Assert.Equal(allergyTest.IdListaAlergia, result.First().IdListaAlergia);
        //    Assert.Equal(allergyTest.IdExpediente, result.First().IdExpediente);
        //}

        [Fact]
        public async void InsertAllergyAsync()
        {
            //var dbSet = new Mock<DbSet<Alergias>>();
            var mockRepo = new Mock<IAlergyRepository>();
            var mockRepoList = new Mock<IAlergyListRepository>();
            var allergyTest = new Alergias
            {
                IdExpediente = 1,
                IdListaAlergia = 1,
                FechaCreacion = DateTime.Now
            };
            IAlergyService AllergyService = new AlergyService(mockRepo.Object, mockRepoList.Object);
            var result = await AllergyService.InsertAllergyAsync(allergyTest);
            Assert.Equal(allergyTest.IdListaAlergia, result.IdListaAlergia);
            Assert.Equal(allergyTest.IdExpediente, result.IdExpediente);
        }

        [Fact]
        public async void GetAllAsyncNull()
        {
            var mockRepo = new Mock<IAlergyRepository>();
            var mockRepoList = new Mock<IAlergyListRepository>();
            mockRepoList.Setup(p => p.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ListaAlergia>>(null));
            IAlergyService AllergyService = new AlergyService(mockRepo.Object, mockRepoList.Object);
            var result = await AllergyService.GetAll();
            Assert.Null(result);
        }
    }
}

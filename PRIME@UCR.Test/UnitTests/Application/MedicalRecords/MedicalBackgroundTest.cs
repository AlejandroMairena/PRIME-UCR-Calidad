using System.Collections.Generic;
using Xunit;
using Moq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Application.Implementations.MedicalRecords;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;
using System;
using System.Linq;
using PRIME_UCR.Application.Services.MedicalRecords;

namespace PRIME_UCR.Test.UnitTests.Application.MedicalRecords
{
    public class MedicalBackgroundTest
    {
        //[Fact]
        //public async void getMedicalBackgroundByRecordIdNull()
        //{
        //    var mockRepo = new Mock<IMedicalBackgroundRepository>();
        //    var mockRepoList = new Mock<IMedicalBackgroundListRepository>();
        //    mockRepo.Setup(p => p.GetByConditionAsync(i => i.IdExpediente == 0)).Returns(Task.FromResult<IEnumerable<Antecedentes>>(null));
        //    IMedicalBackgroundService MedicalBackgroundService = 
        //        new MedicalBackgroundService(mockRepo.Object, mockRepoList.Object);
        //    var result = await mockRepo.Object.GetByConditionAsync(a => a.IdExpediente == 0);
        //    var result2 = (await MedicalBackgroundService.GetBackgroundByRecordId(0));
        //    Assert.Null(result);
        //    Assert.Null(result2);
        //}

        [Fact]
        public async void getMedicalBackgroundByRecordIdInvalid()
        {
            var mockRepo = new Mock<IMedicalBackgroundRepository>();
            var mockRepoList = new Mock<IMedicalBackgroundListRepository>();
            IMedicalBackgroundService MedicalBackgroundService =
                new MedicalBackgroundService(mockRepo.Object, mockRepoList.Object);
            var result = (await MedicalBackgroundService.GetBackgroundByRecordId(-1));
            Assert.Empty(result);
        }

        [Fact]
        public async void getMedicalBackgroundByRecordIdReturnsValidMedicalBackground()
        {
            var mockRepo = new Mock<IMedicalBackgroundRepository>();
            var mockRepoList = new Mock<IMedicalBackgroundListRepository>();
            var MedicalBackgroundTest = new Antecedentes
            {
                IdExpediente = 1,
                IdListaAntecedentes = 1,
                FechaCreacion = DateTime.Now
            };
            var MedicalBackgroundList = new List<Antecedentes>
            {
                MedicalBackgroundTest
            };
            IEnumerable<Antecedentes> MedicalBackgroundEnumerable = MedicalBackgroundList;
            mockRepo
             .Setup(p => p.GetByConditionAsync(i => i.IdExpediente == 1))
             .Returns(Task.FromResult(MedicalBackgroundEnumerable));
            IMedicalBackgroundService MedicalBackgroundService = new MedicalBackgroundService(mockRepo.Object, mockRepoList.Object);
            var result = (await mockRepo.Object.GetByConditionAsync(i => i.IdExpediente == 1)).ToList();
            var result2 = (await MedicalBackgroundService.GetBackgroundByRecordId(1)).ToList();
            Assert.Equal(MedicalBackgroundTest.IdListaAntecedentes, result.First().IdListaAntecedentes);
            Assert.Equal(MedicalBackgroundTest.IdExpediente, result.First().IdExpediente);
        }

        [Fact]
        public async void InsertMedicalBackgroundAsync()
        {
            //var dbSet = new Mock<DbSet<Antecedentes>>();
            var mockRepo = new Mock<IMedicalBackgroundRepository>();
            var mockRepoList = new Mock<IMedicalBackgroundListRepository>();
            var MedicalBackgroundTest = new Antecedentes
            {
                IdExpediente = 1,
                IdListaAntecedentes = 1,
                FechaCreacion = DateTime.Now
            };
            IMedicalBackgroundService MedicalBackgroundService = new MedicalBackgroundService(mockRepo.Object, mockRepoList.Object);
            var result = await MedicalBackgroundService.InsertBackgroundAsync(MedicalBackgroundTest);
            Assert.Equal(MedicalBackgroundTest.IdListaAntecedentes, result.IdListaAntecedentes);
            Assert.Equal(MedicalBackgroundTest.IdExpediente, result.IdExpediente);
        }

        [Fact]
        public async void GetAllAsyncNull()
        {
            var mockRepo = new Mock<IMedicalBackgroundRepository>();
            var mockRepoList = new Mock<IMedicalBackgroundListRepository>();
            mockRepoList.Setup(p => p.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ListaAntecedentes>>(null));
            IMedicalBackgroundService MedicalBackgroundService = new MedicalBackgroundService(mockRepo.Object, mockRepoList.Object);
            var result = await MedicalBackgroundService.GetAll();
            Assert.Null(result);
        }
    }
}


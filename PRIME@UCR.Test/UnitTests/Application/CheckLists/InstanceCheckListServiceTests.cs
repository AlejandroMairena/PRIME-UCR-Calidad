using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PRIME_UCR.Application.Implementations.CheckLists;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;
using Xunit;
using System;

namespace PRIME_UCR.Test.UnitTests.Application.CheckLists
{
    public class InstanceCheckListServiceTests
    {
        [Fact]
        public async Task GetNumberOfItemsReturnsZero()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>();
            mockRepo
                .Setup(p => p.GetByIncidentCodAndCheckListId("código válido", 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetNumberOfItems("código válido", 1);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetNumberOfItemsReturnsValid()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>
            {
                new InstanciaItem {Completado = true, ItemId = 1, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = true, ItemId = 2, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = false, ItemId = 3, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = false, ItemId = 4, PlantillaId = 1, IncidentCod = "código válido"}
            };
            mockRepo
                .Setup(p => p.GetByIncidentCodAndCheckListId("código válido", 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetNumberOfItems("código válido", 1);

            // assert
            Assert.Equal(4, result);
        }

        [Fact]
        public async Task GetNumberOfCompletedItemsReturnsZero()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>
            {
                new InstanciaItem {Completado = false, ItemId = 1, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = false, ItemId = 2, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = false, ItemId = 3, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = false, ItemId = 4, PlantillaId = 1, IncidentCod = "código válido"}
            };
            mockRepo
                .Setup(p => p.GetByIncidentCodAndCheckListId("código válido", 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetNumberOfCompletedItems("código válido", 1);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetNumberOfCompletedItemsReturnsValid()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>
            {
                new InstanciaItem {Completado = true, ItemId = 1, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = false, ItemId = 2, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = true, ItemId = 3, PlantillaId = 1, IncidentCod = "código válido"},
                new InstanciaItem {Completado = false, ItemId = 4, PlantillaId = 1, IncidentCod = "código válido"}
            };
            mockRepo
                .Setup(p => p.GetByIncidentCodAndCheckListId("código válido", 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetNumberOfCompletedItems("código válido", 1);

            // assert
            Assert.Equal(2, result);
        }
    }
}

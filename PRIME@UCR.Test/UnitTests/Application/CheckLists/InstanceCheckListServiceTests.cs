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

        [Fact]
        public async Task GetCoreItemsReturnsEmpty()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>();
            mockRepo
                .Setup(p => p.GetCoreItems("código válido", 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetCoreItems("código válido", 1);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCoreItemsReturnsValid()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>
            {
                new InstanciaItem {ItemId = 1, PlantillaId = 1, IncidentCod = "código válido", ItemPadreId = null},
                new InstanciaItem {ItemId = 3, PlantillaId = 1, IncidentCod = "código válido", ItemPadreId = null}
            };
            mockRepo
                .Setup(p => p.GetCoreItems("código válido", 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetCoreItems("código válido", 1);

            // assert
            Assert.Collection(result,
                                    itemInstance => Assert.Equal(1, itemInstance.ItemId),
                                    itemInstance => Assert.Equal(3, itemInstance.ItemId)
                            );
        }


        [Fact]
        public async Task GetItemsByFatherIdReturnsEmpty()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>();
            mockRepo
                .Setup(p => p.GetItemsByFatherId("código válido", 1, 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetItemsByFatherId("código válido", 1, 1);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetItemsByFatherIdReturnsValid()
        {
            // arrange
            var mockRepo = new Mock<IInstanceItemRepository>();
            var data = new List<InstanciaItem>
            {
                new InstanciaItem {ItemId = 2, PlantillaId = 1, IncidentCod = "código válido", ItemPadreId = 1},
                new InstanciaItem {ItemId = 3, PlantillaId = 1, IncidentCod = "código válido", ItemPadreId = 1}
            };
            mockRepo
                .Setup(p => p.GetItemsByFatherId("código válido", 1, 1))
                .Returns(Task.FromResult<IEnumerable<InstanciaItem>>(data));
            var service = new InstanceChecklistService(
                null, mockRepo.Object);

            // act
            var result = await service.GetItemsByFatherId("código válido", 1, 1);

            // assert
            Assert.Collection(result,
                                    itemInstance => Assert.Equal(2, itemInstance.ItemId),
                                    itemInstance => Assert.Equal(3, itemInstance.ItemId)
                            );
        }
    }
}

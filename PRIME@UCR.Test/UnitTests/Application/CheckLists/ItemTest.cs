using Moq;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Application.Services.CheckLists;
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
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Test.UnitTests.Application.CheckLists
{
    public class ItemTest
    {
        [Fact]
        public void GetItemByNameEmptyTest()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByName(String.Empty))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemByNameTest()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByName("Asegurar TET, corrugado o interfase de VMNI"))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemByNameIncorrectTest()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByName("Asegurar TET"))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemByCheckListTest()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByCheckListId(0))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemByCheckListTestIncorrect()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByCheckListId(-1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemBySuperItemTest()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByCheckListId(24))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemBySuperItemTestIncorrect()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.InsertCheckItemAsync(new Item()))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemByIDServiceTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetItemById(0))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemByIDServiceTestIncorrect()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetItemById(-1))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void InsertItemServiceTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.InsertCheckListItem(new Item()))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void InsertItemServiceIncorrectTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.InsertCheckListItem(null))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemByChecklistIDServiceTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetItemsByCheckListId(1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemByChecklistIDServiceTestIncorrect()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetItemsByCheckListId(-1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemBySuperItemIDServiceTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetItemsBySuperitemId(24))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemBySuperItemIDService2Test()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetItemsBySuperitemId(1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemBySuperItemIDServiceTestIncorrect()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetItemsBySuperitemId(-1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetCoreItemsTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetCoreItems(1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetCoreItemsTestIncorrect()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.GetCoreItems(-1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void InsertImageTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.SaveImageItem(new Item()))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void InsertImageTestIncorrect()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.SaveImageItem(null))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void UpdateItemTest()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.UpdateItem(new Item()))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void UpdateItemTestIncorrect()
        {
            var mockRepo = new Mock<ICheckListService>();
            mockRepo.Setup(p => p.UpdateItem(null))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }
    }
}

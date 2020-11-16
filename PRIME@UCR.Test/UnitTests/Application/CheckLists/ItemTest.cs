using Moq;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models.CheckLists;
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

namespace PRIME_UCR.Test.UnitTests.Application.CheckLists
{
    public class ItemTest
    {
        [Fact]
        public void GetItemByNameEmpty()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByName(String.Empty))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemByName()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByName("Asegurar TET, corrugado o interfase de VMNI"))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemByNameIncorrect()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByName("Asegurar TET"))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemByCheckList()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByCheckListId(0))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemByCheckListIncorrect()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByCheckListId(-1))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.Null(mockRepo.Object);
        }

        [Fact]
        public void GetItemBySuperItem()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(p => p.GetByCheckListId(24))
                .Returns(Task.FromResult<IEnumerable<Item>>(new List<Item>()));
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void GetItemBySuperItemIncorrect()
        {
            var mockRepo = new Mock<IItemRepository>();
            new Item();
            mockRepo.Setup(p => p.InsertCheckItemAsync(new Item()))
                .Returns(Task.FromResult<Item>(new Item()));
            Assert.NotNull(mockRepo.Object);
        }
    }
}

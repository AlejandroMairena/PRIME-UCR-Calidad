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
    public class CheckListServiceTests
    {
        [Fact] 
        public async Task GetAllReturnsEmpty()
        {
            // arrange
            var mockRepo = new Mock<ICheckListRepository>();
            var data = new List<CheckList>();
            mockRepo
                .Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<CheckList>>(data));
            var service = new CheckListService(
                mockRepo.Object, null, null);

            // act
            var result = await service.GetAll();

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllReturnsValidList()
        {
            // arrange
            var mockRepo = new Mock<ICheckListRepository>();
            var data = new List<CheckList>
            {
                new CheckList {Id = 1},
                new CheckList {Id = 2},
                new CheckList {Id = 3},
                new CheckList {Id = 4}
            };
            mockRepo
                .Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<CheckList>>(data));
            var service = new CheckListService(
                mockRepo.Object, null, null);

            // act
            var result = await service.GetAll();

            // assert
            Assert.Collection(result,
                                    checkList => Assert.Equal(1, checkList.Id),
                                    checkList => Assert.Equal(2, checkList.Id),
                                    checkList => Assert.Equal(3, checkList.Id),
                                    checkList => Assert.Equal(4, checkList.Id)
                            );
        }

        [Fact]
        public async Task GetTypesReturnsEmpty()
        {
            // arrange
            var mockRepo = new Mock<ICheckListTypeRepository>();
            var data = new List<TipoListaChequeo>();
            mockRepo
                .Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<TipoListaChequeo>>(data));
            var service = new CheckListService(
                null, mockRepo.Object, null);

            // act
            var result = await service.GetTypes();

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTypesReturnsValidList()
        {
            // arrange
            var mockRepo = new Mock<ICheckListTypeRepository>();
            var data = new List<TipoListaChequeo>
            {
                new TipoListaChequeo {Nombre = "Colocación equipo"},
                new TipoListaChequeo {Nombre = "Retiro equipo"},
                new TipoListaChequeo {Nombre = "Paciente en origen"},
                new TipoListaChequeo {Nombre = "Paciente en destino"},
                new TipoListaChequeo {Nombre = "Paciente en traslado"}
            };
            mockRepo
                .Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<TipoListaChequeo>>(data));
            var service = new CheckListService(
                null, mockRepo.Object, null);

            // act
            var result = await service.GetTypes();

            // assert
            Assert.Collection(result,
                                    type => Assert.Equal("Colocación equipo", type.Nombre),
                                    type => Assert.Equal("Retiro equipo", type.Nombre),
                                    type => Assert.Equal("Paciente en origen", type.Nombre),
                                    type => Assert.Equal("Paciente en destino", type.Nombre),
                                    type => Assert.Equal("Paciente en traslado", type.Nombre)
                            );
        }

        [Fact]
        public async Task GetByIdReturnsNull()
        {
            // arrange
            var mockRepo = new Mock<ICheckListRepository>();
            CheckList data = null;
            mockRepo
                .Setup(p => p.GetByKeyAsync(1))
                .Returns(Task.FromResult(data));
            var service = new CheckListService(
                mockRepo.Object, null, null);

            // act
            var result = await service.GetById(1);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdReturnsValid()
        {
            // arrange
            var mockRepo = new Mock<ICheckListRepository>();
            CheckList data = new CheckList { Id = 1 };
            mockRepo
                .Setup(p => p.GetByKeyAsync(1))
                .Returns(Task.FromResult(data));
            var service = new CheckListService(
                mockRepo.Object, null, null);

            // act
            var result = await service.GetById(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}

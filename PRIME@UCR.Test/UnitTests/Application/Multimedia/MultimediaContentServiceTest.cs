using Moq;
using PRIME_UCR.Application.Implementations.Multimedia;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.UnitTests.Application.Multimedia
{
    public class MultimediaContentServiceTest
    {
        [Fact]
        public async Task GetByIdTest()
        {
            var mockRepo = new Mock<IMultimediaContentRepository>();
            mockRepo.Setup(s => s.GetByKeyAsync(1))
            .Returns(Task.FromResult(new MultimediaContent
            {
                Id = 1
            }));
            var mockService = new MultimediaContentService(mockRepo.Object, null, null);
            var result = await mockService.GetById(1);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddMultContToActionTest()
        {
            var mockActionRepo = new Mock<IActionRepository>();
            Accion a_test = new Accion
            {
                MultContId = 1,
                CitaId = 1,
                NombreAccion = "Test"
            };
            mockActionRepo.Setup(r => r.InsertAsync(a_test))
                .Returns(Task.FromResult(a_test));
            var mockService = new MultimediaContentService(null, mockActionRepo.Object, null);
            var result = await mockService.AddMultContToAction(1, "Test", 1);
            Assert.Equal(1, result.MultContId);
            Assert.Equal(1, result.CitaId);
            Assert.Equal("Test", result.NombreAccion);
        }


        [Fact]
        public async Task GetByAppointmentActionTest()
        {
            var list = new List<MultimediaContent>
            {
                new MultimediaContent
                {
                    Id = 1
                }
            };
            var mockActionRepo = new Mock<IActionRepository>();
            mockActionRepo.Setup(r => r.GetByAppointmentAction(1, "Mock"))
                .Returns(Task.FromResult(list.AsEnumerable()));
            var mockService = new MultimediaContentService(null, mockActionRepo.Object, null);
            var result = (await mockService.GetByAppointmentAction(1, "Mock"));
            Assert.Collection(result, m => Assert.Equal(1, m.Id));
        }

        [Fact]
        public async Task AddMultimediaContentTest()
        { 
            var mockRepo = new Mock<IMultimediaContentRepository>();
            MultimediaContent mc = new MultimediaContent
            {
                Id = 1,
                Nombre = "test",
                Archivo = "ruta",
                Fecha_Hora = DateTime.Now,
                Tipo = "test"
            };
            mockRepo.Setup(r => r.InsertAsync(mc))
                .Returns(Task.FromResult(mc));
            var mockService = new MultimediaContentService(mockRepo.Object, null, null);
            var result = await mockService.AddMultimediaContent(mc);
            Assert.Equal(mc.Id, result.Id);
            Assert.Equal(mc.Nombre, result.Nombre);
            Assert.Equal(mc.Archivo, result.Archivo);
            Assert.Equal(mc.Fecha_Hora, result.Fecha_Hora);
            Assert.Equal(mc.Tipo, result.Tipo);
        }

        [Fact]
        public async Task GetByCheckListItemTest()
        {
            var list = new List<MultimediaContent>
            {
                new MultimediaContent
                {
                    Id = 1
                }
            };
            var mockMCItemRepo = new Mock<IMultimediaContentItemRepository>();
            mockMCItemRepo.Setup(r => r.GetByCheckListItem(1, 1, "COD"))
                .Returns(Task.FromResult(list.AsEnumerable()));
            var mockService = new MultimediaContentService(null, null, mockMCItemRepo.Object);
            var result = await mockService.GetByCheckListItem(1, 1, "COD");
            Assert.Collection(result, m => Assert.Equal(1, m.Id));
        }

    }
}

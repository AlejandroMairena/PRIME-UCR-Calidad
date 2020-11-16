using Moq;
using PRIME_UCR.Application.Implementations.Multimedia;
using PRIME_UCR.Application.Repositories.Appointments;
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
        public async Task GetByAppointmentActionTest()
        {
            var list = new List<MultimediaContent>
            {
                new MultimediaContent
                {
                    Id = 1
                }
            };
            list.AsEnumerable();
            var mockActionRepo = new Mock<IActionRepository>();
            mockActionRepo.Setup(r => r.GetByAppointmentAction(1, "Mock"))
                .Returns(Task.FromResult(list.AsEnumerable()));
            var mockService = new MultimediaContentService(null, mockActionRepo.Object, null);
            var result = (await mockService.GetByAppointmentAction(1, "Mock"));
            Assert.Collection(result, m => Assert.Equal(1, m.Id));
        }


    }
}

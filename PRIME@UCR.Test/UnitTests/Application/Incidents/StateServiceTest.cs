using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.Implementations.Incidents;

namespace PRIME_UCR.Test.UnitTests.Application.Incidents
{
    public class StateServiceTest
    {
        [Fact]
        public async Task GetAllStatesTestNull()
        {
            var mockRepo = new Mock<IStateRepository>();
            mockRepo.Setup(u => u.GetAllStates()).Returns(Task.FromResult<IEnumerable<Estado>>(new List<Estado>()));

            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(StateService), "GetAllStates"));

            var stateService = new StateService(mockRepo.Object, mockSecurity.Object);
            var result = await stateService.GetAllStates();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllStatesTestNotNull()
        {
            var mockRepo = new Mock<IStateRepository>();
            List<Estado> stateList = new List<Estado>
            {
                new Estado(),
                new Estado(),
                new Estado()
            };
            mockRepo.Setup(s => s.GetAllStates()).Returns(Task.FromResult<IEnumerable<Estado>>(stateList));

            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(StateService), "GetAllStates"));

            var stateService = new StateService(mockRepo.Object, mockSecurity.Object);
            var result = await stateService.GetAllStates();

            Assert.NotEmpty(result);
        }
    }
}

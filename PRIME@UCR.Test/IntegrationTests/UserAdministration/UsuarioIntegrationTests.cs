using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.IntegrationTests.UserAdministration
{
    public class UsuarioIntegrationTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;
        public UsuarioIntegrationTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllUsersWithDetailsAsyncReturnsNotNull()
        {
            var usuarioService = _factory.Services.GetRequiredService<IUserService>();
            var result = await usuarioService.GetAllUsersWithDetailsAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPersonWithDetailstAsyncReturnsValue ()
        {
            var usuarioService = _factory.Services.GetRequiredService<IUserService>();
            var result = await usuarioService.getPersonWithDetailstAsync("juan.guzman@prime.com");
            var ced = result.Cédula;
            Assert.Equal("12345678", ced);
        }

        [Fact]
        public async Task GetPersonWithDetailstAsyncReturnsNull()
        {
            var usuarioService = _factory.Services.GetRequiredService<IUserService>();
            var result = await usuarioService.getPersonWithDetailstAsync("invalid value");
            Assert.Null(result);
        }

        [Fact]
        public async Task GetNotAuthenticatedUsersReturnsNotNull()
        {
            var usuarioService = _factory.Services.GetRequiredService<IUserService>();
            var result = await usuarioService.GetNotAuthenticatedUsers();
            var value = result.Find(c => c.CedPersona == "11111111");
            Assert.NotNull(value);
        }

        [Fact]
        public async Task StoreUserAsyncReturnsFalse()
        {
            var usuarioService = _factory.Services.GetRequiredService<IUserService>();
            var userToRegist = new UserFormModel
            {
                Email = "juan.guzman@prime.com",
                IdCardNumber = "12345678",
            };
            var value = await usuarioService.StoreUserAsync(userToRegist);
            Assert.False(value);
        }

        [Fact]
        public async Task StoreUserAsyncReturnsTrue()
        {
            var personaService = _factory.Services.GetRequiredService<IPersonService>();

            var personToRegist = new PersonFormModel
            {
                IdCardNumber = "117980341",
                Name = "Luis",
                FirstLastName = "Sanchez",
                SecondLastName = "Romero"
            };
            await personaService.StoreNewPersonAsync(personToRegist);

            var usuarioService = _factory.Services.GetRequiredService<IUserService>();
            var userToRegist = new UserFormModel
            {
                Email = "luisandres2712@gmail.com",
                IdCardNumber = "117980341",
            };
            var value = await usuarioService.StoreUserAsync(userToRegist);
            Assert.True(value);
        }

    }
}
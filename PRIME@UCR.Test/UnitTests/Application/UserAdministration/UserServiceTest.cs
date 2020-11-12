using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Domain.Models.UserAdministration;
using System.Reflection;
using System.Threading.Tasks;

namespace PRIME_UCR.Test.UnitTests.Application.UserAdministration
{
    public class UserServiceTest
    {
      
        [Fact]
        public async void getUsuarioWithDetailsReturnsNull()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(p => p.GetWithDetailsAsync(String.Empty)).Returns(Task.FromResult<Usuario>(null));
            var store = new Mock<IUserStore<Usuario>>();
            var mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object);
            var result = await userService.getUsuarioWithDetails(String.Empty);
            Assert.Null(result);
        }

        [Fact]
        public async void getUsuarioWithDetailsReturnsValidUser()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo
                .Setup(p => p.GetWithDetailsAsync("a6f7aa70-a038-419f-9945-7c77b093d58f"))
                .Returns(Task.FromResult<Usuario>(new Usuario
                {
                    Id = "a6f7aa70-a038-419f-9945-7c77b093d58f",
                    UserName = "juan.guzman@prime.com",
                    NormalizedUserName = "JUAN.GUZMAN@PRIME.COM",
                    Email = "juan.guzman@prime.com",
                    NormalizedEmail = "JUAN.GUZMAN@PRIME.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEKBfjZVSMkEvJ3kJikd/FETuy1hxI3csK3qM2EwHBlQpgixfBX3tUaxpposHbUfakg==",
                    SecurityStamp = "M7SUOG4MXMPBKLX2BN34HVOG7GRGNIDQ",
                    ConcurrencyStamp = "8caf2844-e5ad-452c-b89f-016d71b5d09e",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                })) ;
            var store = new Mock<IUserStore<Usuario>>();
            var mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object);
            var result = await userService.getUsuarioWithDetails("a6f7aa70-a038-419f-9945-7c77b093d58f");
            Assert.Equal("a6f7aa70-a038-419f-9945-7c77b093d58f" , result.Id);
            Assert.Equal("juan.guzman@prime.com", result.Email);
            Assert.Equal("AQAAAAEAACcQAAAAEKBfjZVSMkEvJ3kJikd/FETuy1hxI3csK3qM2EwHBlQpgixfBX3tUaxpposHbUfakg==" , result.PasswordHash);
            Assert.Equal("M7SUOG4MXMPBKLX2BN34HVOG7GRGNIDQ" , result.SecurityStamp);
        }

    }
}

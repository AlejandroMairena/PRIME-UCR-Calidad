﻿using PRIME_UCR.Application.Implementations.UserAdministration;
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
using PRIME_UCR.Application.Services.UserAdministration;

namespace PRIME_UCR.Test.UnitTests.Application.UserAdministration
{
    public class UserServiceTest
    {
        /*
        [Fact]
        public async void getUsuarioWithDetailsReturnsNull()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(p => p.GetWithDetailsAsync(String.Empty)).Returns(Task.FromResult<Usuario>(null));
            var store = new Mock<IUserStore<Usuario>>();
            var mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(UsersService), "getUsuarioWithDetailsAsync"));
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object, mockSecurity.Object);
            var result = await userService.getUsuarioWithDetailsAsync(String.Empty);
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
            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(UsersService), "getUsuarioWithDetailsAsync"));
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object, mockSecurity.Object);
            var result = await userService.getUsuarioWithDetailsAsync("a6f7aa70-a038-419f-9945-7c77b093d58f");
            Assert.Equal("a6f7aa70-a038-419f-9945-7c77b093d58f" , result.Id);
            Assert.Equal("juan.guzman@prime.com", result.Email);
            Assert.Equal("AQAAAAEAACcQAAAAEKBfjZVSMkEvJ3kJikd/FETuy1hxI3csK3qM2EwHBlQpgixfBX3tUaxpposHbUfakg==" , result.PasswordHash);
            Assert.Equal("M7SUOG4MXMPBKLX2BN34HVOG7GRGNIDQ" , result.SecurityStamp);
        }

        [Fact]
        public async void GetAllUsersWithDetailsAsyncNoUsersTest()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(u => u.GetAllUsersWithDetailsAsync()).ReturnsAsync(new List<Usuario>());
            var store = new Mock<IUserStore<Usuario>>();
            var mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(UsersService), "getUsuarioWithDetailsAsync"));
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object, mockSecurity.Object);
            var result = await userService.GetAllUsersWithDetailsAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllUsersWithDetailsAsyncFiveUsersTest()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(u => u.GetAllUsersWithDetailsAsync()).ReturnsAsync(
                new List<Usuario>() { 
                    new Usuario()
                    {
                        UserName = "luis.sanchez@prime.com"
                    },
                    new Usuario()
                    {
                        UserName = "daniela.vargas@prime.com"
                    },
                    new Usuario()
                    {
                        UserName = "fernando.morales@prime.com"
                    },
                    new Usuario()
                    {
                        UserName = "jose.viquez@prime.com"
                    },
                    new Usuario()
                    {
                        UserName = "elian.vargas@prime.com"
                    },
                });
            var store = new Mock<IUserStore<Usuario>>();
            var mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(UsersService), "getUsuarioWithDetailsAsync"));
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object, mockSecurity.Object);
            var result = await userService.GetAllUsersWithDetailsAsync();
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async void getPersonWithDetailstAsyncNoUserTest()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(u => u.GetUserByEmailAsync(String.Empty)).Returns(Task.FromResult<Usuario>(null));
            var store = new Mock<IUserStore<Usuario>>();
            var mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(UsersService), "getPersonWithDetailstAsync"));
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object, mockSecurity.Object);
            var result = await userService.getPersonWithDetailstAsync(String.Empty);
            Assert.Null(result);
        }

        [Fact]
        public async void getPersonWithDetailstAsyncUserTest()
        {
            var store = new Mock<IUserStore<Usuario>>();
            var mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(p => p.FindByEmailAsync("luis.sanchez@prime.com")).Returns(Task.FromResult(new Usuario()
            {
                Id = "test-test",
                UserName = "luis.sanchez@prime.com",
                CedPersona = "12345678"
            }));
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(u => u.GetWithDetailsAsync("test-test")).Returns(Task.FromResult<Usuario>(new Usuario() { 
                UserName = "luis.sanchez@prime.com",
                CedPersona = "12345678",
                Persona = new Persona()
                {
                    Cédula = "12345678",
                    Nombre = "Luis"
                }
            }));
            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(UsersService), "getPersonWithDetailstAsync"));
            var userService = new UsersService(mockRepo.Object, mockUserManager.Object, mockSecurity.Object);
            var result = await userService.getPersonWithDetailstAsync("luis.sanchez@prime.com");
            Assert.Equal("12345678",result.Cédula);
        }
    }
}

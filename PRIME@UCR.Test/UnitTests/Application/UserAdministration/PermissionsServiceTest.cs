﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.Implementations.UserAdministration;
using System.Linq;

namespace PRIME_UCR.Test.UnitTests.Application.UserAdministration
{
    public class PermissionsServiceTest
    {
        [Fact]
        public async void getPermisosTestNull()
        {
            var mockRepo = new Mock<IPermisoRepository>();
            mockRepo.Setup(u => u.GetAllAsync()).Returns(Task.FromResult(new List<Permiso>()));

            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(PermissionsService), "GetPermisos"));

            var permissionService = new PermissionsService(mockRepo.Object, mockSecurity.Object);
            var result = await permissionService.GetPermisos();

            Assert.Empty(result);
        }

        [Fact]
        public async void getPermisosTestNotNull()
        {
            var mockRepo = new Mock<IPermisoRepository>();
            mockRepo.Setup(u => u.GetAllAsync()).Returns(Task.FromResult(new List<Permiso>()
            { 
                new Permiso()
                {
                    IDPermiso = 1,
                    DescripciónPermiso = "Test1",
                },
                new Permiso()
                {
                    IDPermiso = 2,
                    DescripciónPermiso = "Test3",
                },
                new Permiso()
                {
                    IDPermiso = 3,
                    DescripciónPermiso = "Test3",
                }
            }));

            var mockSecurity = new Mock<IPrimeSecurityService>();
            mockSecurity.Setup(s => s.CheckIfIsAuthorizedAsync(typeof(PermissionsService), "GetPermisos"));

            var permissionService = new PermissionsService(mockRepo.Object, mockSecurity.Object);
            var result = await permissionService.GetPermisos();

            Assert.Equal(3, result.ToList().Count);
        }
    }
}

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.UnitTests.Application.UserAdministration
{
    public class CustomAuthenticationStateProviderTest
    {
        [Fact]
        public async void GetClaimIdentityNullTest()
        {
            var sessionMock = new Mock<ISessionStorageService>();
            var store = new Mock<IUserStore<Usuario>>();
            var userManagerMock = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Usuario>>().Object,
                new IUserValidator<Usuario>[0],
                new IPasswordValidator<Usuario>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Usuario>>>().Object);
            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<Usuario>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object);
            var authenticationServiceMock = new Mock<PRIME_UCR.Application.Services.UserAdministration.IAuthenticationService>();
            
            sessionMock.Setup(s => s.GetItemAsync<string>("emailAddress")).Returns(Task.FromResult(String.Empty));
            authenticationServiceMock.Setup(a => a.GetUserByEmailAsync(String.Empty));
            authenticationServiceMock.Setup(a => a.GetAllProfilesWithDetailsAsync()).Returns(Task.FromResult(new List<Perfil>()));

            var customAuthenticationStateProvider = new CustomAuthenticationStateProvider(signInManagerMock.Object, 
                userManagerMock.Object, 
                sessionMock.Object,
                authenticationServiceMock.Object);

            var result = await customAuthenticationStateProvider.GetAuthenticationStateAsync();
            Assert.Null(result.User.Identity.Name);
        }

        [Fact]
        public async void GetClaimIdentityNotNullTest()
        {
            var sessionMock = new Mock<ISessionStorageService>();
            var store = new Mock<IUserStore<Usuario>>();
            var userManagerMock = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Usuario>>().Object,
                new IUserValidator<Usuario>[0],
                new IPasswordValidator<Usuario>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Usuario>>>().Object);
            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<Usuario>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object);
            var authenticationServiceMock = new Mock<PRIME_UCR.Application.Services.UserAdministration.IAuthenticationService>();
            
            sessionMock.Setup(s => s.GetItemAsync<string>("emailAddress")).Returns(Task.FromResult("test@test.com"));
            authenticationServiceMock.Setup(a => a.GetUserByEmailAsync("test@test.com")).Returns(Task.FromResult(new Usuario
            {
                Email = "test@test.com",
                Id = "test-test",
                UsuariosYPerfiles = new List<Pertenece>()
                {
                    new Pertenece()
                    {
                        IDPerfil = "Admin",
                        IDUsuario = "test-test",
                        Perfil = new Perfil()
                        {
                            NombrePerfil = "Admin"
                        }
                    },
                    new Pertenece()
                    {
                        IDPerfil = "Medico",
                        IDUsuario = "test-test",
                        Perfil = new Perfil()
                        {
                            NombrePerfil = "Admin"
                        }
                    }
                }
            }));
            authenticationServiceMock.Setup(a => a.GetAllProfilesWithDetailsAsync()).Returns(Task.FromResult(new List<Perfil>() {
                new Perfil()
                {
                    NombrePerfil = "Admin",
                    PerfilesYPermisos = new List<Permite>()
                    {
                        new Permite()
                        {
                            IDPerfil = "Admin",
                            IDPermiso = 1,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 1
                            }
                        },
                        new Permite()
                        {
                            IDPerfil = "Admin",
                            IDPermiso = 2,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 2
                            }
                        }
                    }
                },
                new Perfil()
                {
                    NombrePerfil = "Medico",
                    PerfilesYPermisos = new List<Permite>()
                    {
                        new Permite()
                        {
                            IDPerfil = "Medico",
                            IDPermiso = 3,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 3
                            }
                        },
                        new Permite()
                        {
                            IDPerfil = "Medico",
                            IDPermiso = 4,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 4
                            }
                        }
                    }
                },
            }));

            var customAuthenticationStateProvider = new CustomAuthenticationStateProvider(signInManagerMock.Object,
                userManagerMock.Object,
                sessionMock.Object,
                authenticationServiceMock.Object);

            var result = await customAuthenticationStateProvider.GetAuthenticationStateAsync();

            Assert.Equal("test@test.com",result.User.Identity.Name);
            Assert.True(result.User.Identity.IsAuthenticated);
            Assert.Equal(21, result.User.Claims.ToList().Count);
            for (var permission = 1; permission <= 4; ++permission)
            {
                Assert.Equal("true", result.User.Claims.ToList()[permission].Value);
            }
            for(var permission = 5; permission < 21; ++permission)
            {
                Assert.Equal("false", result.User.Claims.ToList()[permission].Value);
            }
        }

        [Fact]
        public async void AuthenticateLoginOnValidSubmit()
        {
            var sessionMock = new Mock<ISessionStorageService>();
            var store = new Mock<IUserStore<Usuario>>();
            var userManagerMock = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Usuario>>().Object,
                new IUserValidator<Usuario>[0],
                new IPasswordValidator<Usuario>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Usuario>>>().Object);
            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<Usuario>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object);
            var authenticationServiceMock = new Mock<PRIME_UCR.Application.Services.UserAdministration.IAuthenticationService>();

            userManagerMock.Setup(u => u.FindByEmailAsync("test@test.com")).Returns(Task.FromResult(new Usuario
            {
                Id = "test-test",
                UserName = "test@test.com",
                Email = "test@test.com",
                PasswordHash = "Test.1234"
            }));

            authenticationServiceMock.Setup(a => a.GetUserWithDetailsAsync("test-test")).Returns(Task.FromResult(new Usuario
            {
                Id = "test-test",
                UserName = "test@test.com",
                Email = "test@test.com",
                PasswordHash = "Test.1234",
                UsuariosYPerfiles = new List<Pertenece>()
                {
                    new Pertenece()
                    {
                        IDPerfil = "Admin",
                        IDUsuario = "test-test",
                        Perfil = new Perfil()
                        {
                            NombrePerfil = "Admin"
                        }
                    },
                    new Pertenece()
                    {
                        IDPerfil = "Medico",
                        IDUsuario = "test-test",
                        Perfil = new Perfil()
                        {
                            NombrePerfil = "Admin"
                        }
                    }
                }
            }));

            authenticationServiceMock.Setup(a => a.GetAllProfilesWithDetailsAsync()).Returns(Task.FromResult(new List<Perfil>() {
                new Perfil()
                {
                    NombrePerfil = "Admin",
                    PerfilesYPermisos = new List<Permite>()
                    {
                        new Permite()
                        {
                            IDPerfil = "Admin",
                            IDPermiso = 1,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 1
                            }
                        },
                        new Permite()
                        {
                            IDPerfil = "Admin",
                            IDPermiso = 2,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 2
                            }
                        }
                    }
                },
                new Perfil()
                {
                    NombrePerfil = "Medico",
                    PerfilesYPermisos = new List<Permite>()
                    {
                        new Permite()
                        {
                            IDPerfil = "Medico",
                            IDPermiso = 3,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 3
                            }
                        },
                        new Permite()
                        {
                            IDPerfil = "Medico",
                            IDPermiso = 4,
                            Permiso = new Permiso()
                            {
                                IDPermiso = 4
                            }
                        }
                    }
                },
            }));

            signInManagerMock.Setup(s => s.CheckPasswordSignInAsync(It.IsAny<Usuario>(), "Test.1234", false)).Returns(Task.FromResult(SignInResult.Success));


            var customAuthenticationStateProvider = new CustomAuthenticationStateProvider(signInManagerMock.Object,
                userManagerMock.Object,
                sessionMock.Object,
                authenticationServiceMock.Object);

            var result = await customAuthenticationStateProvider.AuthenticateLogin(new LogInModel
            {
                Correo = "test@test.com",
                Contraseña = "Test.1234"
            });

            Assert.True(result);
        }

        [Fact]
        public async void AuthenticateLoginOnInvalidUserSubmit()
        {
            var sessionMock = new Mock<ISessionStorageService>();
            var store = new Mock<IUserStore<Usuario>>();
            var userManagerMock = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Usuario>>().Object,
                new IUserValidator<Usuario>[0],
                new IPasswordValidator<Usuario>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Usuario>>>().Object);
            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<Usuario>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object);
            var authenticationServiceMock = new Mock<PRIME_UCR.Application.Services.UserAdministration.IAuthenticationService>();

            userManagerMock.Setup(u => u.FindByEmailAsync(String.Empty)).Returns(Task.FromResult<Usuario>(null));

            var customAuthenticationStateProvider = new CustomAuthenticationStateProvider(signInManagerMock.Object,
                userManagerMock.Object,
                sessionMock.Object,
                authenticationServiceMock.Object);

            var result = await customAuthenticationStateProvider.AuthenticateLogin(new LogInModel
            {
                Correo = String.Empty,
                Contraseña = String.Empty
            });

            Assert.False(result);
        }

        [Fact]
        public async void AuthenticateLoginOnInvalidPasswordSubmit()
        {
            var sessionMock = new Mock<ISessionStorageService>();
            var store = new Mock<IUserStore<Usuario>>();
            var userManagerMock = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Usuario>>().Object,
                new IUserValidator<Usuario>[0],
                new IPasswordValidator<Usuario>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Usuario>>>().Object);
            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<Usuario>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object);
            var authenticationServiceMock = new Mock<PRIME_UCR.Application.Services.UserAdministration.IAuthenticationService>();

            userManagerMock.Setup(u => u.FindByEmailAsync("test@test.com")).Returns(Task.FromResult(new Usuario
            {
                Id = "test-test",
                UserName = "test@test.com",
                Email = "test@test.com",
                PasswordHash = "Test.1234"
            }));

            signInManagerMock.Setup(s => s.CheckPasswordSignInAsync(It.IsAny<Usuario>(), String.Empty, false)).Returns(Task.FromResult(SignInResult.Failed));

            var customAuthenticationStateProvider = new CustomAuthenticationStateProvider(signInManagerMock.Object,
                userManagerMock.Object,
                sessionMock.Object,
                authenticationServiceMock.Object);

            var result = await customAuthenticationStateProvider.AuthenticateLogin(new LogInModel
            {
                Correo = "test@test.com",
                Contraseña = ""
            });

            Assert.False(result);
        }
        [Fact]
        public async void LogoutTest()
        {
            var sessionMock = new Mock<ISessionStorageService>();
            var store = new Mock<IUserStore<Usuario>>();
            var userManagerMock = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Usuario>>().Object,
                new IUserValidator<Usuario>[0],
                new IPasswordValidator<Usuario>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Usuario>>>().Object);
            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<Usuario>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object);
            var authenticationServiceMock = new Mock<PRIME_UCR.Application.Services.UserAdministration.IAuthenticationService>();

            sessionMock.Setup(s => s.RemoveItemAsync(It.IsAny<string>()));

            var customAuthenticationStateProvider = new CustomAuthenticationStateProvider(signInManagerMock.Object,
                userManagerMock.Object,
                sessionMock.Object,
                authenticationServiceMock.Object);

            var result = await customAuthenticationStateProvider.Logout();

            Assert.True(result);
        }
    }
}
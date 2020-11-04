using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PerteneceRepository : IPerteneceRepository
    {
        private readonly IPrimeSecurityService _primeSecurityService;

        private readonly ISqlDataProvider _db;

        public PerteneceRepository(ISqlDataProvider dataProvider,
            IPrimeSecurityService primeSecurityService)
        {
            _db = dataProvider;
            _primeSecurityService = primeSecurityService;

        }

        public async Task DeleteUserFromProfileAsync(string idUser, string idProfile)
        {
            if ((await _primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                await Task.Run(() =>
                {
                    using (var cmd = _db.DbConnection.CreateCommand())
                    {
                        while (cmd.Connection.State != System.Data.ConnectionState.Open)
                        {
                            cmd.Connection.Open();
                        }
                        if (cmd.Connection.State == System.Data.ConnectionState.Open)
                        {
                            cmd.CommandText =
                                $"EXECUTE dbo.DeleteUserFromProfile @idUser='{idUser}', @idProfile='{idProfile}'";

                            cmd.ExecuteNonQuery();
                        }

                    }
                });
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task InsertUserToProfileAsync(string idUser, string idProfile)
        {
            if((await _primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                await Task.Run(() =>
                {
                    using (var cmd = _db.DbConnection.CreateCommand())
                    {
                        while (cmd.Connection.State != System.Data.ConnectionState.Open)
                        {
                            cmd.Connection.Open();
                        }
                        if (cmd.Connection.State == System.Data.ConnectionState.Open)
                        {
                            cmd.CommandText =
                                $"EXECUTE dbo.InsertUserToProfile @idUsuario='{idUser}', @nombrePerfil='{idProfile}'";

                            cmd.ExecuteNonQuery();
                        }

                    }
                });
            } else
            {
                throw new NotAuthorizedException();
            }

        }
    }
}

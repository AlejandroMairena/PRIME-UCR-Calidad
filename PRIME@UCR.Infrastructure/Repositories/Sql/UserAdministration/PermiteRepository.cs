using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System.Threading.Tasks;
using System.Data.Common;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PermiteRepository : IPermiteRepository
    {
        
        private readonly ISqlDataProvider _db;

        private readonly IPrimeSecurityService primeSecurityService;

        public PermiteRepository(ISqlDataProvider dataProvider, 
            IPrimeSecurityService _primeSecurityService)
        {
            _db = dataProvider;
            primeSecurityService = _primeSecurityService;
        }

        public async Task DeletePermissionAsync(string idProfile, int idPermission) 
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
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
                                $"EXECUTE dbo.DeletePermissionFromProfile @idPermission='{idPermission}', @idProfile='{idProfile}'";

                            cmd.ExecuteNonQuery();
                        }
                    
                    }
                });
            } else
            {
                throw new NotAuthorizedException();
            }

        }
        public async Task InsertPermissionAsync(string idProfile, int idPermission)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
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
                                $"EXECUTE dbo.InsertPermissionToProfile @idPermission='{idPermission}', @idProfile='{idProfile}'";

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
    }
}

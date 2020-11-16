using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.Permissions.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Dashboard
{
    public partial class DashboardRepository : IDashboardRepository
    {
        public readonly ISqlDataProvider _db;
        public readonly IPrimeSecurityService primeSecurity;

        public DashboardRepository(ISqlDataProvider sqlDataProvider, IPrimeSecurityService securityService)
        {
            _db = sqlDataProvider;
            primeSecurity = securityService;
        }

        public async Task<int> GetIncidentsCounterAsync(string modality)
        {
            await primeSecurity.CheckIfIsAuthorizedAsync(this.GetType());
            var result = 0;
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
                            $"EXECUTE dbo.GetIncidentsCounter @modality='{modality}'";

                        var dbResult = cmd.ExecuteScalar();
                        result = int.Parse(dbResult.ToString());

                    }

                }
            });

            return await Task.FromResult(result);
        }
    }

    [MetadataType(typeof(DashboardRepositoryPermissions))]
    public partial class DashboardRepository
    {
        
    }
}

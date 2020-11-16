using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.Permissions.Dashboard;
using RepoDb;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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

        /**
         * Method used to get the list of all the incidents.
         * 
         * Return: List of incidents.
         */
        public async Task<List<Incidente>> GetAllIncidentsAsync()
        {
            var incidents = new List<Incidente>();
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                var extractor = await connection.ExecuteQueryMultipleAsync(@"
                    SELECT *
                    FROM Incidente;

                    SELECT *
                    FROM Cita;
                ");

                incidents = extractor.Extract<Incidente>().AsList();
                var appointments = extractor.Extract<Cita>().AsList();

                foreach(var incident in incidents)
                {
                    incident.Cita = appointments.FirstOrDefault(a => a.Id == incident.CodigoCita);
                }

            }
            return incidents;

        }

        /**
         * Method used to get the list of all the incidents join with origin information.
         * 
         * Return: List of incidents.
         */
        public async Task<List<Distrito>> GetAllDistrictsAsync()
        {
            var result = new List<Distrito>();
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                result = (await connection.QueryAllAsync<Distrito>()).AsList();
            }
            return result;
        }

        public Task<Incidente> GetByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Incidente>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Incidente>> GetByConditionAsync(Expression<Func<Incidente, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Incidente> InsertAsync(Incidente model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Incidente model)
        {
            throw new NotImplementedException();
        }
    }

    [MetadataType(typeof(DashboardRepositoryPermissions))]
    public partial class DashboardRepository
    {
        
    }
}

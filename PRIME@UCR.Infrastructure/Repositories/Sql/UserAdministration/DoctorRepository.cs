using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using RepoDb;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System.Reflection;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ISqlDataProvider _db;

        private readonly IPrimeSecurityService primeSecurityService;

        public DoctorRepository(ISqlDataProvider dataProvider,
            IPrimeSecurityService _primeSecurityService)
        {
            _db = dataProvider;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<Médico> GetByKeyAsync(string key)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            using (var connection = new SqlConnection(_db.DbConnection.ConnectionString))
            {
                var result = await connection.ExecuteQueryAsync<Médico>($@"
                    select Persona.Cédula, Nombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento from Persona
                    join Médico M on Persona.Cédula = M.Cédula
                    where M.Cédula = {key}
                ");
                
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Médico>> GetAllAsync()
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            using (var connection = new SqlConnection(_db.DbConnection.ConnectionString))
            {
                return await connection.ExecuteQueryAsync<Médico>($@"
                    select Persona.Cédula, Nombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento from Persona
                    join Médico M on Persona.Cédula = M.Cédula
                ");
            }
        }

        public async Task<IEnumerable<Médico>> GetByConditionAsync(Expression<Func<Médico, bool>> expression)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            using (var connection = new SqlConnection(_db.DbConnection.ConnectionString))
            {
                return await connection.QueryAsync(expression);
            }
        }

        public async Task<Médico> InsertAsync(Médico model)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            using (var connection = new SqlConnection(_db.DbConnection.ConnectionString))
            {
                var result = (await connection.QueryAsync<Persona>(model.Cédula)).FirstOrDefault();
                if (result == null)
                {
                    await connection.InsertAsync<Persona>(model);
                }
                
                await connection.InsertAsync(nameof(Médico), new {model.Cédula});
                return model;
            }
        }

        public async Task DeleteAsync(string key)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            using (var connection = new SqlConnection(_db.DbConnection.ConnectionString))
            {
                await connection.DeleteAsync(nameof(Médico), key as object);
            }
        }

        public async Task UpdateAsync(Médico model)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            using (var connection = new SqlConnection(_db.DbConnection.ConnectionString))
            {
                await connection.UpdateAsync(nameof(Médico), new {model.Cédula});
            }
        }
    }
}

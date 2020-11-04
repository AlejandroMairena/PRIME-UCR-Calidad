using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepoDb;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ISqlDataProvider _db;
        private readonly IPrimeSecurityService primeSecurityService;

        public PacienteRepository(ISqlDataProvider dataProvider,
            IPrimeSecurityService _primeSecurityService)
        {
            _db = dataProvider;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<Paciente> InsertPatientOnlyAsync(Paciente entity)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {

                await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
                await connection.ExecuteNonQueryAsync(
                "dbo.InsertarPacienteSolo",
                new { Cedula = entity.Cédula },
                CommandType.StoredProcedure);

                return entity;
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task<Paciente> GetByKeyAsync(string key)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {
                await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
                var result = await connection.ExecuteQueryAsync<Paciente>(@"
                select Paciente.Cédula, Nombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento from Persona
                join Paciente on Persona.Cédula = Paciente.Cédula
                where Paciente.Cédula = @Ced
                ", new { Ced = key });
                return result.FirstOrDefault();
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {
                await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
                var result = await connection.ExecuteQueryAsync<Paciente>(@"
                select Paciente.Cédula, Nombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento from Persona
                join Paciente on Persona.Cédula = Paciente.Cédula
                ");
                return result;
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task<IEnumerable<Paciente>> GetByConditionAsync(Expression<Func<Paciente, bool>> expression)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {
                await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
                return await connection.QueryAsync(expression);
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task<Paciente> InsertAsync(Paciente model)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {
                await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
                var result = (await connection.QueryAsync<Persona>(model.Cédula)).FirstOrDefault();
                if (result == null)
                {
                    await connection.InsertAsync<Persona>(model);
                }
            
                await connection.InsertAsync(nameof(Paciente), new {model.Cédula});
                return model;
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task DeleteAsync(string key)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {
                await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
                await connection.DeleteAsync(nameof(Paciente), key as object);
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task UpdateAsync(Paciente model)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {
                await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
                await connection.UpdateAsync(nameof(Paciente), new {model.Cédula});
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }
    }
}

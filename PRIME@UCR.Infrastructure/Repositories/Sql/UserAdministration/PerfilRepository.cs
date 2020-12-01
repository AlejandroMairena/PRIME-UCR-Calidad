using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;
using RepoDb; 
using RepoDb.Extensions;
using System.Data.SqlClient; 
using System.Linq;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PerfilRepository :  IPerfilRepository
    {
        protected readonly ISqlDataProvider _db;
        public PerfilRepository(ISqlDataProvider dataProvider) 
        {
            _db = dataProvider;
        }

        public Task<List<Perfil>> GetPerfilesWithDetailsAsync()
        {
            return _db.Profiles
                .Include(p => p.FuncionariosYPerfiles)
                .ThenInclude(p => p.Funcionario)
                .Include(p => p.PerfilesYPermisos)
                .ThenInclude(p => p.Permiso)
                .Include(p => p.UsuariosYPerfiles)
                .ThenInclude(p => p.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        /*
         * Function: Determines if an user exists in AdministradorCentroDeControl table in the Database
         * @Params: The user´s id (Cedula)
         * @Return: True if it exists in the table; False otherwise
         * @Story ID: PIG01IIC20-712
         */
        public async Task<bool> IsAdministratorAsync(string id) 
        {
            using (var connection = new SqlConnection(_db.ConnectionString)) 
            { 
                IEnumerable<AdministradorCentroDeControl> admin = await connection.QueryAsync<AdministradorCentroDeControl>(id);
                return admin.Count() != 0;
            } 
        }

        /*
         * Function: Determines if an user exists in CoordinadorTécnicoMédico table in the Database
         * @Params: The user´s id (Cedula)
         * @Return: True if it exists in the table; False otherwise
         * @Story ID: PIG01IIC20-712
         */
        public async Task<bool> IsCoordinatorAsync(string id)
        {
            using (var connection = new SqlConnection(_db.ConnectionString)) 
            { 
                IEnumerable<CoordinadorTécnicoMédico> coordinator = await connection.QueryAsync<CoordinadorTécnicoMédico>(id);
                return coordinator.Count() != 0;
            } 
        }

        /*
         * Function: Determines if an user exists in Médico table in the Database
         * @Params: The user´s id (Cedula)
         * @Return: True if it exists in the table; False otherwise
         * @Story ID: PIG01IIC20-712
         */
        public async Task<bool> IsDoctorAsync(string id)
        {
            using (var connection = new SqlConnection(_db.ConnectionString)) 
            { 
                IEnumerable<Médico> doctor = await connection.QueryAsync<Médico>(id);
                return doctor.Count() != 0;
            } 
        }

        /*
         * Function: Determines if an user exists in EspecialistaTécnicoMédico table in the database
         * @Params: The user´s id (Cedula)
         * @Return: True if it exists in the table; False otherwise
         * @Story ID: PIG01IIC20-712
         */
        public async Task<bool> IsTechnicalSpecialistAsync(string id)
        {
            using (var connection = new SqlConnection(_db.ConnectionString)) 
            { 
                IEnumerable<EspecialistaTécnicoMédico> specialist = await connection.QueryAsync<EspecialistaTécnicoMédico>(id);
                return specialist.Count() != 0;
            } 
        }
    }
}


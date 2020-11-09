using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ISqlDataProvider _db;

        public AuthenticationRepository(ISqlDataProvider dataProvider)
        {
            _db = dataProvider;
        }

        public async Task<List<Usuario>> GetAllUsersWithDetailsAsync()
        {
            return await _db.Usuarios
                    .Include(u => u.Persona)
                    .Include(u => u.UsuariosYPerfiles)
                    .ToListAsync();
        }

        public async Task<List<Perfil>> GetPerfilesWithDetailsAsync()
        {
            return await _db.Profiles
                .Include(p => p.FuncionariosYPerfiles)
                .ThenInclude(p => p.Funcionario)
                .Include(p => p.PerfilesYPermisos)
                .ThenInclude(p => p.Permiso)
                .Include(p => p.UsuariosYPerfiles)
                .ThenInclude(p => p.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Usuario> GetUserByEmailAsync(string email)
        {
            return _db.Usuarios
                .Include(u => u.UsuariosYPerfiles)
                .Include(u => u.Persona)
                .AsNoTracking()
                .FirstAsync(u => u.Email == email);
        }

        public Task<Usuario> GetWithDetailsAsync(string id)
        {
            return _db.Usuarios
                .Include(u => u.UsuariosYPerfiles)
                .Include(u => u.Persona)
                .AsNoTracking()
                .FirstAsync(u => u.Id == id);
        }
    }
}

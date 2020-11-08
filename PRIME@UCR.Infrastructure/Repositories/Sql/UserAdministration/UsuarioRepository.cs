using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.Permissions.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public partial class UsuarioRepository : IUsuarioRepository
    {

        private readonly ISqlDataProvider _db;

        private readonly IPrimeSecurityService _primeSecurityService;

        public UsuarioRepository(ISqlDataProvider dataProvider, IPrimeSecurityService primeSecurityService)
        {
            _db = dataProvider;
            _primeSecurityService = primeSecurityService;
        }

        /**
         * Method used to get the list of all users with details.
         * 
         * Return: List of users with details.
         */
        public async Task<List<Usuario>> GetAllUsersWithDetailsAsync()
        {
            return await _db.Usuarios
            .Include(u => u.Persona)
            .Include(u => u.UsuariosYPerfiles)
            .ThenInclude(p => p.Perfil)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _db.Usuarios
            .Include(u => u.Persona)
            .Include(u => u.UsuariosYPerfiles)
            .FirstAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetWithDetailsAsync(string id)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _db.Usuarios
            .Include(u => u.UsuariosYPerfiles)
            .Include(u => u.Persona)
            .FirstAsync(u => u.Id == id);
        }
    }

    [MetadataType(typeof(UsuarioRepositoryAuthorization))]
    public partial class UsuarioRepository
    {
    }
}

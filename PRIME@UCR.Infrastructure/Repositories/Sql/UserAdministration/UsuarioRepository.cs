using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class UsuarioRepository : GenericRepository<Usuario, string>, IUsuarioRepository
    {
        public UsuarioRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
            
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
                .ToListAsync();
        }

        public async Task<Usuario> GetWithDetailsAsync(string id)
        {
            return await _db.Usuarios
                .Include(u => u.UsuariosYPerfiles)
                .Include(u => u.Persona)
                .FirstAsync(u => u.Id == id);
        }
    }
}

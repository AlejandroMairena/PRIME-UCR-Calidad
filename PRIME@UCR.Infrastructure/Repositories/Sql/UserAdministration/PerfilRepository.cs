using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PerfilRepository : GenericRepository<Perfil, string>, IPerfilRepository
    {
        public PerfilRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {

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
    }
}

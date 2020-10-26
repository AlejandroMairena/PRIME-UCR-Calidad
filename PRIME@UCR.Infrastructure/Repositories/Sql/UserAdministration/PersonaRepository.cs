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
    public class PersonaRepository : GenericRepository<Persona, string>, IPersonaRepository
    {
        public PersonaRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<Persona> GetByKeyPersonaAsync(string id)
        {
            return await _db.People.FindAsync(id);
        }

        public async Task<Persona> GetWithDetailsAsync(string id)
        {
            return await _db.People
                .Include(i => i.Cédula)
                .Include(i => i.Nombre)
                .Include(i => i.PrimerApellido)
                .Include(i => i.SegundoApellido)
                .Include(i=> i.Sexo)
                .FirstOrDefaultAsync(i => i.Cédula == id);
        }
    }
}

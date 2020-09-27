using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class ModesRepository : IModesRepository
    {
        private readonly ISqlDataProvider _db;

        public ModesRepository(ISqlDataProvider db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Modalidad>> GetAllAsync()
        {
            return await _db.Modes.AsNoTracking().ToListAsync();
        }
    }
}
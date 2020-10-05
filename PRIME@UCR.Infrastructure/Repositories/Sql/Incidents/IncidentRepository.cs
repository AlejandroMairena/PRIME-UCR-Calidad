using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class IncidentRepository : GenericRepository<Incidente, int>, IIncidentRepository
    {
        public IncidentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<Incidente> GetWithDetailsAsync(string code)
        {
            return await _db.Incidents
                .Include(i => i.Origen)
                .Include(i => i.Destino)
                .FirstOrDefaultAsync(i => i.Codigo == code);
        }
    }
}
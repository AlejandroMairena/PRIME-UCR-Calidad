using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class TransportUnitRepository : GenericRepository<UnidadDeTransporte, string>, ITransportUnitRepository
    {
        public TransportUnitRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<UnidadDeTransporte>> GetAllTransporUnitsByMode(string mode)
        {
            return await _db.TransportUnits
                .AsNoTracking()
                .Where(t => t.Modalidad == mode)
                .ToListAsync();
        }

        public async Task<UnidadDeTransporte> GetTransporUnitByIncidentIdAsync(string incidentId)
        {
            return await _db.Incidents
                .Include(i => i.UnidadDeTransporte)
                .Where(i => i.Codigo == incidentId)
                .Select(i => i.UnidadDeTransporte)
                .FirstOrDefaultAsync();
        }
    }
}

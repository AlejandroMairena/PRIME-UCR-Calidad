using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class IncidentStateRepository : IIncidentStateRepository
    {
        private readonly ISqlDataProvider _db;

        public IncidentStateRepository(ISqlDataProvider db)
        {
            _db = db;
        }

        public async Task AddState(EstadoIncidente incidentState)
        {
            await _db.IncidentStates
                .Where(state => state.CodigoIncidente == incidentState.CodigoIncidente)
                .ForEachAsync(state => state.Activo = false);
            _db.IncidentStates.Add(incidentState);
            await _db.SaveChangesAsync();
        }
    }
}
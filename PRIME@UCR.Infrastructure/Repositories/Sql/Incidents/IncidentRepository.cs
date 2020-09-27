using System;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class IncidentRepository : GenericRepository<Incidente, string>, IIncidentRepository
    {
        public IncidentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
    }
}
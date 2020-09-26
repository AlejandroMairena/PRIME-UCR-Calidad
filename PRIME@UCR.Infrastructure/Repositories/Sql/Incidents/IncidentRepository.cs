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
        
        // testing purposes only
        public new Task<Incidente> GetByKeyAsync(string key)
        {
            return Task.FromResult(
                new Incidente
                {
                    Id = key,
                    Estado = "Creado",
                    Modalidad = new Modalidad { Tipo = "Marítimo" }
                }
            );
        }
    }
}
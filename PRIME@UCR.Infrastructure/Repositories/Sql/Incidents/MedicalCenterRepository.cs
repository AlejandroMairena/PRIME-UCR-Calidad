using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class MedicalCenterRepository : GenericRepository<CentroMedico, int>, IMedicalCenterRepository
    {
        public MedicalCenterRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public new Task<IEnumerable<CentroMedico>> GetAllAsync()
        {
            _db.MedicalCenters
                .Include(mc => mc.Distrito)
                .ThenInclude(d => d.Canton)
                .ThenInclude(c => c.Provincia)
                .ThenInclude(p => p.Pais);
            IEnumerable<CentroMedico> result = new List<CentroMedico>
            {
                new CentroMedico { Id = 1, Latitud = 0.0, Longitud = 1.0, Nombre = "Hospital Cima" },
                new CentroMedico { Id = 2, Latitud = 1.0, Longitud = 2.0, Nombre = "Hospital Calderón Guardia" }
            };
            return Task.FromResult(result);
        }

    }
}
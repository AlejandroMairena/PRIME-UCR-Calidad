using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class MedicalCenterRepository : GenericRepository<CentroMedico, int>, IMedicalCenterRepository
    {
        public MedicalCenterRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<Médico>> GetDoctorsByMedicalCenterId(int id)
        {
            return await _db.WorksOn
                .AsNoTracking()
                .Include(w => w.Médico)
                .Where(w => w.CentroMedicoId == id)
                .Select(w => w.Médico)
                .ToListAsync();
        }
    }
}
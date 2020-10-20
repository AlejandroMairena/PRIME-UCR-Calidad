using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;


namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    class WorksOnMedicalCenterRepository : IWorksOnMedicalCenterRepository
    {
        private readonly ISqlDataProvider _db;

        public WorksOnMedicalCenterRepository(ISqlDataProvider db)
        {
            _db = db;
        }
        public async Task<IEnumerable<TrabajaEn>> GetAllDoctorsbyMedicalCenterId(int medicalCenterId)
        {
            return await _db.WorksOn
                .AsNoTracking()
                .Where(w => w.CentroMedicoId == medicalCenterId)
                .ToListAsync();
        }
    }
}

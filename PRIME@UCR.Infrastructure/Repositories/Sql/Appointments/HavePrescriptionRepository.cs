using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Domain.Models.Appointments;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Appointments
{
    public class HavePrescriptionRepository : GenericRepository<PoseeReceta, int>, IHavePrescriptionRepository
    {
        public HavePrescriptionRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {

        }

        public async Task<IEnumerable<PoseeReceta>> GetPrescriptionByAppointmentId(int id) {
            return await _db.HavePrescription
                .Include(p => p.RecetaMedica)
                .Where(p => p.IdCitaMedica == id)
                .ToListAsync();
        }

    }
}

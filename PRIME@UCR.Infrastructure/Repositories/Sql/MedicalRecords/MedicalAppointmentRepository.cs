using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Domain.Models.Appointments;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords
{
    public class MedicalAppointmentRepository : GenericRepository<CitaMedica, int>, IMedicalAppointmentRepository
    {
        public MedicalAppointmentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {

        }

        public async Task<CitaMedica> GetByAppointmentId(int id)
        {
            return await _db.MedicalAppointment.FirstOrDefaultAsync(ap => ap.CitaId == id); 
        }

    }
}

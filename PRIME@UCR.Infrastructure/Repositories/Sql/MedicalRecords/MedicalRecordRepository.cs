using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords
{
    public class MedicalRecordRepository : GenericRepository<Expediente, int>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<Expediente> GetByPatientIdAsync(string id)
        {
            return await _db.MedicalRecords.FirstOrDefaultAsync(mr => mr.CedulaPaciente == id);
        }

        public async Task<IEnumerable<Expediente>> GetByNameAndLastnameAsync(string name, string lastname) {

            return await _db.MedicalRecords
                .Include(p => p.Paciente)
                .Where(p => p.Paciente.Nombre == name && p.Paciente.PrimerApellido == lastname)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Expediente>> GetByNameAndLastnameLastnameAsync(string name, string lastname, string lastname2) {

            return await _db.MedicalRecords
                .Include(p => p.Paciente)
                .Where(p => p.Paciente.Nombre == name && p.Paciente.PrimerApellido == lastname && p.Paciente.SegundoApellido == lastname2)
                .ToListAsync(); 

        }


        public async Task<Expediente> GetWithDetailsAsync(int id)
        {
            return await _db.MedicalRecords
                .Include(i => i.CedulaPaciente)
                .Include(i => i.CedulaMedicoDuenno)
                .Include(i => i.Id)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
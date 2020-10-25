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
    }
}
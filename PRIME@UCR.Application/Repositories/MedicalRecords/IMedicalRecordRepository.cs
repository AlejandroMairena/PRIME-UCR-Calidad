using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Repositories.MedicalRecords
{
    public interface IMedicalRecordRepository : IGenericRepository<Expediente, int>
    {
        Task<Expediente> GetByPatientIdAsync(string id);
    }

}
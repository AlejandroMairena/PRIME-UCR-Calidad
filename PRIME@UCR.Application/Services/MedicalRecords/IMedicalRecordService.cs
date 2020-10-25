using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Services.MedicalRecords
{
    public interface IMedicalRecordService
    {
        // returns null if no such record exists
        Task<Expediente> GetByPatientIdAsync(string id);
        Task<Expediente> CreateMedicalRecordAsync(Expediente entity);
    }
}
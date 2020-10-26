using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Application.DTOs.MedicalRecords;

namespace PRIME_UCR.Application.Services.MedicalRecords
{
    public interface IMedicalRecordService
    {
        // returns null if no such record exists
        Task<Expediente> GetByPatientIdAsync(string id);
        Task<Expediente> CreateMedicalRecordAsync(Expediente entity);
        Task<Expediente> GetByIdAsync(int id);

        Task<RecordViewModel> GetIncidentDetailsAsync(int id);
    }
}
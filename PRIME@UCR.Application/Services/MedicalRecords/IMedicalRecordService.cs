using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Services.MedicalRecords
{
    public interface IMedicalRecordService
    {
        // returns null if no such record exists
        Task<Expediente> GetByPatientIdAsync(string id);

        Task<IEnumerable<Expediente>> GetAllAsync();

        Task<IEnumerable<Expediente>> GeyByConditionAsync(string name);

        Task<Expediente> InsertAsync(Expediente expediente); 

        Task<Expediente> CreateMedicalRecordAsync(Expediente entity);
    }
}
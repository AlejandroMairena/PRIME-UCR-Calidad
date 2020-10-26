using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Repositories.MedicalRecords
{
    public interface IMedicalRecordRepository : IGenericRepository<Expediente, int>
    {
        Task<Expediente> GetByPatientIdAsync(string id);

        Task<IEnumerable<Expediente>> GetByNameAndLastnameAsync(string name, string lastname);

        Task<IEnumerable<Expediente>> GetByNameAndLastnameLastnameAsync(string name, string lastname, string lastname2);
    }

}
using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Application.Services.Multimedia;
using System.Linq;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Application.Repositories.MedicalRecords;

namespace PRIME_UCR.Application.Implementations.MedicalRecords
{
    public class AlergyService : IAlergyService
    {
        private readonly IAlergyRepository _repo;

        public AlergyService(IAlergyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Alergia>> GetAlergyByRecordId(int recordId)
        {
            return await _repo.GetByConditionAsync(i => i.IdExpediente == recordId);
        }
    }
}

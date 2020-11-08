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
        public async Task<Alergia> GetByIdAsync(int id, int idExpediente, int idListaAlergias)
        {
            //hacer metodo para retornar valor de llaves conjuntas
            return await _repo.GetByKeyAsync(id,idExpediente,idListaAlergias);
        }
        public async Task<IEnumerable<Alergia>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Alergia> InsertAsync(Alergia alergia)
        {
            return await _repo.InsertAsync(alergia);
        }

        public async Task<Alergia> GetByMedicalRecordIdAsync(int id)
        {
            return await _repo.GetByMedicalRecordIdAsync(id);
        }
    }
}

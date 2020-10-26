using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Implementations.MedicalRecords
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repo;

        public MedicalRecordService(IMedicalRecordRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Expediente>> GeyByConditionAsync(string name) {

            return await _repo.GetByConditionAsync(_repo => _repo.Paciente.Nombre.Contains(name)); 
        }

        public async Task<IEnumerable<Expediente>> GetAllAsync() {

            return await _repo.GetAllAsync(); 
        }


        public async Task<Expediente> GetByPatientIdAsync(string id)
        {
            return await _repo.GetByPatientIdAsync(id);
        }

        public async Task<Expediente> InsertAsync(Expediente expediente) {

            return await _repo.InsertAsync(expediente);
        }

        public async Task<Expediente> CreateMedicalRecordAsync(Expediente entity)
        {
            return await _repo.InsertAsync(entity);
        }
    }
}
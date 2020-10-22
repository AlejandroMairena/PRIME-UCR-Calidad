using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PatientService : IPatientService
    {
        private readonly IPacienteRepository _patientRepo;

        public PatientService(IPacienteRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public async Task<Paciente> GetPatientByIdAsync(string id)
        {
            return await _patientRepo.GetByKeyAsync(id);
        }

        public async Task<Paciente> CreatePatientAsync(Paciente entity)
        {
            return await _patientRepo.InsertAsync(entity);
        }

        public async Task<Paciente> InsertPatientOnlyAsync(Paciente entity)
        {
            return await _patientRepo.InsertPatientOnlyAsync(entity);
        }
    }
}
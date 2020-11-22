using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Permissions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public partial class PatientService : IPatientService
    {
        private readonly IPacienteRepository _patientRepo;
        private readonly IPrimeSecurityService primeSecurityService;

        public PatientService(IPacienteRepository patientRepo,
            IPrimeSecurityService _primeSecurityService)
        {
            _patientRepo = patientRepo;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<Paciente> GetPatientByIdAsync(string id)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _patientRepo.GetByKeyAsync(id);
        }

        public async Task<Paciente> CreatePatientAsync(Paciente entity)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _patientRepo.InsertAsync(entity);
        }

        public async Task<Paciente> InsertPatientOnlyAsync(Paciente entity)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _patientRepo.InsertPatientOnlyAsync(entity);
        }
    }

    [MetadataType(typeof(PatientServiceAuthorization))]
    public partial class PatientService { }
}
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PatientService : IPatientService
    {
        private readonly IPacienteRepository _patientRepo;

        private readonly IAuthorizationService authorizationService;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        public PatientService(IPacienteRepository patientRepo,
            AuthenticationStateProvider _authenticationStateProvider,
            IAuthorizationService _authorizationService)
        {
            _patientRepo = patientRepo;
            authorizationService = _authorizationService;
            authenticationStateProvider = _authenticationStateProvider;
        }

        public async Task<Paciente> GetPatientByIdAsync(string id)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageMedicalRecords.ToString())).Succeeded)
            {
                return await _patientRepo.GetByKeyAsync(id);
            }
            return null;
        }

        public async Task<Paciente> CreatePatientAsync(Paciente entity)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageMedicalRecords.ToString())).Succeeded)
            {
                return await _patientRepo.InsertAsync(entity);
            }
            return null;
        }

        public async Task<Paciente> InsertPatientOnlyAsync(Paciente entity)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageMedicalRecords.ToString())).Succeeded)
            {
                return await _patientRepo.InsertPatientOnlyAsync(entity);
            }
            return null;
        }
    }
}
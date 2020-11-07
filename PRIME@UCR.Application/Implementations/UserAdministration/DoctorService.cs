using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        private readonly IPrimeSecurityService primeSecurityService;

        public DoctorService(IDoctorRepository repository,
            IPrimeSecurityService _primeSecurityService)
        {
            _repository = repository;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<Médico> GetDoctorByIdAsync(string id)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            return await _repository.GetByKeyAsync(id);
        }

        public async Task<IEnumerable<Médico>> GetAllDoctorsAsync()
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            return await _repository.GetAllAsync();
        }
    }
}
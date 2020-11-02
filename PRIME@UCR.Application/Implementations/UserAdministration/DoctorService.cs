using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        private readonly IAuthorizationService authorizationService;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        public DoctorService(IDoctorRepository repository,
            AuthenticationStateProvider _authenticationStateProvider,
            IAuthorizationService _authorizationService)
        {
            _repository = repository;
            authorizationService = _authorizationService;
            authenticationStateProvider = _authenticationStateProvider;
        }

        public async Task<Médico> GetDoctorByIdAsync(string id)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                return await _repository.GetByKeyAsync(id);
            }
            return null;

        }

        public async Task<IEnumerable<Médico>> GetAllDoctorsAsync()
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                return await _repository.GetAllAsync();
            }
            return null;

        }
    }
}
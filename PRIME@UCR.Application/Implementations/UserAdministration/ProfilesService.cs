using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class ProfilesService : IProfilesService
    {
        private readonly IPerfilRepository _profilesRepository;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        private readonly IAuthorizationService authorizationService;

        public ProfilesService(IPerfilRepository profileRepository,
            AuthenticationStateProvider _authenticationStateProvider,
            IAuthorizationService _authorizationService)
        {
            _profilesRepository = profileRepository;
            authenticationStateProvider = _authenticationStateProvider;
            authorizationService = _authorizationService;
        }

        public async Task<List<Perfil>> GetPerfilesWithDetailsAsync()
        {
            return await _profilesRepository.GetPerfilesWithDetailsAsync();
        }
    }
}

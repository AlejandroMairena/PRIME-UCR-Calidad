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

        private readonly IPrimeSecurityService primeSecurityService;

        public ProfilesService(IPerfilRepository profileRepository,
            IPrimeSecurityService _primeSecurityService)
        {
            _profilesRepository = profileRepository;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<List<Perfil>> GetPerfilesWithDetailsAsync()
        {
            return await _profilesRepository.GetPerfilesWithDetailsAsync();
        }
    }



}

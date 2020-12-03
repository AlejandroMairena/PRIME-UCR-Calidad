using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{
    public class SecureProfilesService : IProfilesService
    {
        private readonly ProfilesService profilesService;

        private readonly IPrimeSecurityService primeSecurityService;

        private readonly IPerfilRepository perfilRepository;

        public SecureProfilesService(IPrimeSecurityService _primeSecurityService,
            IPerfilRepository _perfilRepository)
        {
            primeSecurityService = _primeSecurityService;
            perfilRepository = _perfilRepository;
            profilesService = new ProfilesService(perfilRepository);
        }

        public async Task<List<Perfil>> GetPerfilesWithDetailsAsync()
        {
            return await profilesService.GetPerfilesWithDetailsAsync();
        }
    }
}

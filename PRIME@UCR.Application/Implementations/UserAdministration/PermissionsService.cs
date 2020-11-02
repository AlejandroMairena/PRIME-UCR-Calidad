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
    public class PermissionsService : IPermissionsService
    {
        private readonly IPermisoRepository _permissionsRepository;

        private readonly IAuthorizationService authorizationService;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        public PermissionsService(IPermisoRepository permisoRepository,
            IAuthorizationService _authorizationService,
            AuthenticationStateProvider _authenticationStateProvider)
        {
            _permissionsRepository = permisoRepository;
            authenticationStateProvider = _authenticationStateProvider;
            authorizationService = _authorizationService;
        }

        public async Task<IEnumerable<Permiso>> GetPermisos()
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                return await _permissionsRepository.GetAllAsync();
            }
            return null;
        }
    }
}   

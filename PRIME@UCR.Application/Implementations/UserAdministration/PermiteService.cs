using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PermiteService : IPermiteService
    {
        private readonly IPermiteRepository _IPermiteRepository;

        private readonly IAuthorizationService authorizationService;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        public PermiteService(IPermiteRepository IPermiteRepository,
            IAuthorizationService _authorizationService,
            AuthenticationStateProvider _authenticationStateProvider) 
        {
            _IPermiteRepository = IPermiteRepository;
            authenticationStateProvider = _authenticationStateProvider;
            authorizationService = _authorizationService;
        }

        public async Task DeletePermissionAsync(string idProfile, int idPermission)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                await _IPermiteRepository.DeletePermissionAsync(idProfile, idPermission);
            }
        }

        public async Task InsertPermissionAsync(string idProfile, int idPermission)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                await _IPermiteRepository.InsertPermissionAsync(idProfile, idPermission);
            }
        }

    }
}

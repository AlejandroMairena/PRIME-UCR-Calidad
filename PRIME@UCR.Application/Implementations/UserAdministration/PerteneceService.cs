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
    public class PerteneceService : IPerteneceService
    {
        private readonly IPerteneceRepository _perteneceRepository;

        private readonly IAuthorizationService authorizationService;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        public PerteneceService(IPerteneceRepository perteneceRepository,
            IAuthorizationService _authorizationService,
            AuthenticationStateProvider _authenticationStateProvider)
        {
            _perteneceRepository = perteneceRepository;
            authorizationService = _authorizationService;
            authenticationStateProvider = _authenticationStateProvider;
        }

        public async Task DeleteUserOfProfileAsync(string idUser, string idProfile)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if( (await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                await _perteneceRepository.DeleteUserFromProfileAsync(idUser, idProfile);
            }
        }

        public async Task InsertUserOfProfileAsync(string idUser, string idProfile)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                await _perteneceRepository.InsertUserToProfileAsync(idUser, idProfile);
            }
        }
    }
}

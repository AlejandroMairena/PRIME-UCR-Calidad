using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PrimeSecurityService : IPrimeSecurityService
    {
        protected readonly IAuthorizationService authorizationService;

        protected readonly AuthenticationStateProvider authenticationStateProvider;

        public PrimeSecurityService(IAuthorizationService _authorizationService,
            AuthenticationStateProvider _authenticationStateProvider)
        {
            authorizationService = _authorizationService;
            authenticationStateProvider = _authenticationStateProvider;
        }

        public async Task CheckIfIsAuthorizedAsync(List<AuthorizationPermissions> authorizationPermissions, bool areAllNeeded = true)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var isAuthorized = areAllNeeded;
            if(areAllNeeded)
            {
                foreach(var permission in authorizationPermissions)
                {
                    if(!(await authorizationService.AuthorizeAsync(user, permission.ToString())).Succeeded)
                    {
                        isAuthorized = false;
                        break;
                    }

                }
            } else
            {
                foreach (var permission in authorizationPermissions)
                {
                    if ((await authorizationService.AuthorizeAsync(user, permission.ToString())).Succeeded)
                    {
                        isAuthorized = true;
                        break;
                    }

                }
            }
            if(!isAuthorized)
            {
                throw new NotAuthorizedException();
            }
        }
    }
}

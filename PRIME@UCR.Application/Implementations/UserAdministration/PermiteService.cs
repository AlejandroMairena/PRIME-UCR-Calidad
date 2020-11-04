using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
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

        private readonly IPrimeSecurityService primeSecurityService;

        public PermiteService(IPermiteRepository IPermiteRepository,
            IPrimeSecurityService _primeSecurityService) 
        {
            _IPermiteRepository = IPermiteRepository;
            primeSecurityService = _primeSecurityService;
        }

        public async Task DeletePermissionAsync(string idProfile, int idPermission)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                await _IPermiteRepository.DeletePermissionAsync(idProfile, idPermission);
            } else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task InsertPermissionAsync(string idProfile, int idPermission)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                await _IPermiteRepository.InsertPermissionAsync(idProfile, idPermission);
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

    }
}

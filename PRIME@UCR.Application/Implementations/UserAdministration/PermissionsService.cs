using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PermissionsService : IPermissionsService
    {
        private readonly IPermisoRepository _permissionsRepository;

        private readonly IPrimeSecurityService primeSecurityService;

        public PermissionsService(IPermisoRepository permisoRepository,
            IPrimeSecurityService _primeSecurityService)
        {
            _permissionsRepository = permisoRepository;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<IEnumerable<Permiso>> GetPermisos()
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            return await _permissionsRepository.GetAllAsync();
        }
    }
}   

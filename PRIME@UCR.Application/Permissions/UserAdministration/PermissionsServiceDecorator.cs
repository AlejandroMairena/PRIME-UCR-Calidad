﻿using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{
    public class PermissionServiceDecorator : IPermissionsService
    {
        private readonly PermissionsService PermissionService;

        protected readonly IPrimeSecurityService primeSecurityService;

        protected readonly IPermisoRepository permissionsRepository;

        public PermissionServiceDecorator(IPrimeSecurityService _primeSecurityService,
            IPermisoRepository _permisoRepository)
        {
            primeSecurityService = _primeSecurityService;
            permissionsRepository = _permisoRepository;
            PermissionService = new PermissionsService(permissionsRepository);
        }

        public async Task<IEnumerable<Permiso>> GetPermisos()
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(new[] { AuthorizationPermissions.CanManageUsers });
            return await PermissionService.GetPermisos();
        }
    }
}

﻿using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{
    public abstract class PermissionServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task<IEnumerable<Permiso>> GetPermisos();
    }
}

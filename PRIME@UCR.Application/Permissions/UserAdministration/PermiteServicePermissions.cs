using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{
    public abstract class PermiteServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task DeletePermissionAsync(string idProfile, int idPermission);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task InsertPermissionAsync(string idProfile, int idPermission);
    }
}

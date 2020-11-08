using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    public abstract class PermiteRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanModifyUsers })]
        public abstract Task DeletePermissionAsync(string idProfile, int idPermission);

        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task InsertPermissionAsync(string idProfile, int idPermission);
    }
}

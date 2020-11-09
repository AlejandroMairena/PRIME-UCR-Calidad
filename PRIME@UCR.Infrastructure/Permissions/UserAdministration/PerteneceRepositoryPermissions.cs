using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    public abstract class PerteneceRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task DeleteUserFromProfileAsync(string idUser, string idProfile);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task InsertUserToProfileAsync(string idUser, string idProfile);

    }
}

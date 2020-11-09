using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{

    public abstract class PerteneceServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task DeleteUserOfProfileAsync(string idUser, string idProfile);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task InsertUserOfProfileAsync(string idUser, string idProfile);
    }

}

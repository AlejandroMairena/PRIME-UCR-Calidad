using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    [AuthorizationType(typeof(UsuarioRepositoryAuthorization))]
    public partial class UsuarioRepository
    {
    }
    public abstract class UsuarioRepositoryAuthorization
    {
        [RequirePermissions(new[] {AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers})]
        public abstract Task<Usuario> GetUserByEmailAsync(string email);

        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task<Usuario> GetWithDetailsAsync(string id);

    }
}

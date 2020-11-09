using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    public abstract class PersonaRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task DeleteAsync(string cedPersona);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task<Persona> GetByKeyPersonaAsync(string id);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task<Persona> GetWithDetailsAsync(string id);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task InsertAsync(Persona persona);

    }
}

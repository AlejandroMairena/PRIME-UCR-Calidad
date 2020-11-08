using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    public abstract class CoordinadorTécnicoMédicoRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<CoordinadorTécnicoMédico> GetByKeyAsync(string key);

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<IEnumerable<CoordinadorTécnicoMédico>> GetAllAsync();
    }
}


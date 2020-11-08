using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    public abstract class DoctorRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<Médico> GetByKeyAsync(string key);

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<IEnumerable<Médico>> GetAllAsync();

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<IEnumerable<Médico>> GetByConditionAsync(Expression<Func<Médico, bool>> expression);

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<Médico> InsertAsync(Médico model);

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task DeleteAsync(string key);

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task UpdateAsync(Médico model);
    }
}


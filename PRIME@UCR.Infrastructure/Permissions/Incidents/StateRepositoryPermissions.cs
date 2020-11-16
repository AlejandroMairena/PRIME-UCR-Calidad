using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.Incidents
{
    public abstract class StateRepositoryPermissions
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanSeeIncidentsInfoOnDashboard })]
        public abstract Task<IEnumerable<Estado>> GetAllStates();
    }
}

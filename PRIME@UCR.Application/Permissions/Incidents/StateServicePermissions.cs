using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.Incidents
{
    public abstract class StateServicePermissions
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanSeeIncidentsInfoOnDashboard })]
        public abstract Task<IEnumerable<Estado>> GetIncidentCounterAsync();

    }
}

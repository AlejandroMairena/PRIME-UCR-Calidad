using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.Incidents
{
    public abstract class IncidentServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanReviewIncidents })]
        public abstract Task ApproveIncidentAsync(string code, string reviewerId);

        [RequirePermissions(new[] { AuthorizationPermissions.CanReviewIncidents })]
        public abstract Task RejectIncidentAsync(string code, string reviewerId);
    }
}
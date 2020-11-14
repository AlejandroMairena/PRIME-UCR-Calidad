using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Permissions.Incidents
{
    public abstract class IncidentServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanReviewIncidents })]
        public abstract Task ApproveIncidentAsync(string code, string reviewerId);

        [RequirePermissions(new[] { AuthorizationPermissions.CanReviewIncidents })]
        public abstract Task RejectIncidentAsync(string code, string reviewerId);

        [RequirePermissions(new[] { AuthorizationPermissions.CanSeeIncidentsList })]
        public abstract Task<IEnumerable<Incidente>> GetAllAsync();

        [RequirePermissions(new[] { AuthorizationPermissions.CanSeeBasicDetailsOfIncidents })]
        public abstract Task<IncidentDetailsModel> GetIncidentDetailsAsync(string code);

        [RequirePermissions(new[] { AuthorizationPermissions.CanEditBasicDetailsOfIncident })]
        public abstract Task<IncidentDetailsModel> UpdateIncidentDetailsAsync(IncidentDetailsModel model);
    }
}
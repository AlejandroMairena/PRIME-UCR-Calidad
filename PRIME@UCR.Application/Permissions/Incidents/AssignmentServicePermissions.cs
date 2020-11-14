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

    public abstract class  AssignmentServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents})]
        public abstract Task AssignToIncidentAsync(string code, AssignmentModel model);

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<AssignmentModel> GetAssignmentsByIncidentIdAsync(string code); 

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<IEnumerable<UnidadDeTransporte>> GetAllTransportUnitsByMode(string mode); 

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<IEnumerable<CoordinadorTécnicoMédico>> GetCoordinatorsAsync(); 

        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<IEnumerable<EspecialistaTécnicoMédico>> GetSpecialistsAsync(); 


    }
}

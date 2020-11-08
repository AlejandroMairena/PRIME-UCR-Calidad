using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{

    public abstract class PatientServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task<Paciente> GetPatientByIdAsync(string id);

        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task<Paciente> CreatePatientAsync(Paciente entity);

        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task<Paciente> InsertPatientOnlyAsync(Paciente entity);
    }
}

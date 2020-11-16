using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Permissions.Appointments
{

    public abstract class AppointmentServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanEditMedicalInfoOfIncidentsPatient})]
        public abstract Task<Expediente> AssignMedicalRecordAsync(int appointmentId, Paciente patient);
    }
}
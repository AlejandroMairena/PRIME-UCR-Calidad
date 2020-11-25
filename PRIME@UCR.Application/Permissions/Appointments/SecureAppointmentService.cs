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
using PRIME_UCR.Application.Implementations.Appointments;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Services.Appointments;
using PRIME_UCR.Domain.Models.Appointments;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Permissions.Appointments
{

    public class SecureAppointmentService : IAppointmentService
    {
        private readonly IPrimeSecurityService primeSecurityService;

        private readonly AppointmentService appointmentService;

        public SecureAppointmentService(IActionTypeRepository actionTypeRepo,
            IAppointmentRepository appointmentRepository,
            IMedicalRecordRepository medicalRecordRepository,
            IPrimeSecurityService _primeSecurityService)
        {
            appointmentService = new AppointmentService(actionTypeRepo, appointmentRepository, medicalRecordRepository);
            primeSecurityService = _primeSecurityService;

        }

        public async Task<Expediente> AssignMedicalRecordAsync(int appointmentId, Paciente patient)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(new[] { AuthorizationPermissions.CanEditMedicalInfoOfIncidentsPatient });
            return await appointmentService.AssignMedicalRecordAsync(appointmentId, patient);
        }

        public async Task<IEnumerable<TipoAccion>> GetActionTypesAsync(bool isIncident = true)
        {
            return await appointmentService.GetActionTypesAsync(isIncident);
        }

        public async Task<Cita> GetLastAppointmentDateAsync(int id)
        {
            return await appointmentService.GetLastAppointmentDateAsync(id);
        }
    }
}
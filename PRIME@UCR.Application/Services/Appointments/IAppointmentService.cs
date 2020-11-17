using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.Appointments
{
    public interface IAppointmentService
    {
        Task<IEnumerable<TipoAccion>> GetActionTypesAsync(bool isIncident = true);
        Task<Expediente> AssignMedicalRecordAsync(int appointmentId, Paciente patient);
        Task<Cita> GetLastAppointmentDateAsync(int id);
    }
}

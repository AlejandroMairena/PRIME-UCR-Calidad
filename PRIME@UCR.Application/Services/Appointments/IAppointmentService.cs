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

        Task<CitaMedica> GetMedicalAppointmentByAppointmentId(int id);

        Task<IEnumerable<PoseeReceta>> GetPrescriptionsByAppointmentId(int id);

        Task<IEnumerable<RecetaMedica>> GetDrugsAsync();

        Task<IEnumerable<RecetaMedica>> GetDrugsByConditionAsync(string drugname);

        Task<PoseeReceta> GetDrugByConditionAsync(int drug_id); 

        Task<PoseeReceta> InsertPrescription(int idMedicalPrescription, int idMedicalAppointment);

        Task<PoseeReceta> UpdatePrescriptionDosis(int idMedicalPrescription, int idMedicalAppointment, string dosis);

        Task UpdateAsync(PoseeReceta prescription); 
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Appointments;
using PRIME_UCR.Application.Implementations.MedicalRecords;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Services.Appointments;
using PRIME_UCR.Domain.Models.Appointments;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IActionTypeRepository _actionTypeRepo;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public AppointmentService(IActionTypeRepository actionTypeRepo,
            IAppointmentRepository appointmentRepository,
            IMedicalRecordRepository medicalRecordRepository)
        {
            _actionTypeRepo = actionTypeRepo;
            _appointmentRepository = appointmentRepository;
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<IEnumerable<TipoAccion>> GetActionTypesAsync(bool isIncident = true)
        {
            return await _actionTypeRepo.GetByConditionAsync(a => a.EsDeIncidente == true);
        }

        public async Task<Expediente> AssignMedicalRecordAsync(int appointmentId, Paciente patient)
        {
            var appointment = await _appointmentRepository.GetByKeyAsync(appointmentId);
            if (appointment == null)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }
            var record = await _medicalRecordRepository.GetByPatientIdAsync(patient.Cédula);
            
            if (record == null)
            {
                var medicalRecord = new Expediente
                {
                    CedulaPaciente = patient.Cédula,
                    FechaCreacion = DateTime.Now
                };
                record = await _medicalRecordRepository.InsertAsync(medicalRecord);
            }

            appointment.IdExpediente = record.Id;

            await _appointmentRepository.UpdateAsync(appointment);

            return record;
        }

        /*public async Task<IEnumerable<AppointmentModel>> GetAppointmentListModelsAsync()
        {
            return await _actionTypeRepo.GetAllAsync();
        }*/
    }
}
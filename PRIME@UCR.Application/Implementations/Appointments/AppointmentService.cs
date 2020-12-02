﻿using System;
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
using PRIME_UCR.Application.Services.UserAdministration;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Application.Permissions.Appointments;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Implementations.Appointments
{
    public partial class AppointmentService : IAppointmentService
    {
        private readonly IActionTypeRepository _actionTypeRepo;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPrimeSecurityService _primeSecurityService;
        private readonly IMedicalAppointmentRepository _medapprepo;
        private readonly IHavePrescriptionRepository _havepresc;
        private readonly IDrugRepository _drugrepo;
        private readonly IMedCenterRepository _medcenrepo;
        private readonly IMedAppMetricRepository _metapprepo;
        private readonly IMedicalSpecialtyRepository _medspecirepo;
        private readonly ISpecializesRepository _speciarepo;
        //private readonly IAppointmentStatusRepository  _appostatusrep;

        public AppointmentService(IActionTypeRepository actionTypeRepo,
            IAppointmentRepository appointmentRepository,
            IMedicalRecordRepository medicalRecordRepository,
            IPrimeSecurityService primeSecurityService,
            IMedicalAppointmentRepository medapp,
            IHavePrescriptionRepository havepres,
            IDrugRepository drugrep,
            IMedCenterRepository medcenrepo,
            IMedAppMetricRepository metapprepo,
            IMedicalSpecialtyRepository medspeci,
            ISpecializesRepository speciarepo
            /*,IAppointmentStatusRepository appstatusrepo*/)
        {
            _actionTypeRepo = actionTypeRepo;
            _appointmentRepository = appointmentRepository;
            _medicalRecordRepository = medicalRecordRepository;
            _primeSecurityService = primeSecurityService;
            _medapprepo = medapp;
            _havepresc = havepres;
            _drugrepo = drugrep;
            _medcenrepo = medcenrepo;
            _metapprepo = metapprepo;
            _medspecirepo = medspeci;
            _speciarepo = speciarepo;
            //_appostatusrep = appstatusrepo;
        }


        public async Task<MetricasCitaMedica> GetMetricsMedAppointmentByAppId(int id) {
            return await _metapprepo.GetAppMetricsByAppId(id); 
        }

        public async Task<IEnumerable<Persona>> GetDoctorsBySpecialtyNameAsync(string specialty_name) {
            return await _speciarepo.GetDoctorsWithSpecialty(specialty_name);
        }

        public async Task<IEnumerable<EspecialidadMedica>> GetMedicalSpecialtiesAsync() {
            return await _medspecirepo.GetAllAsync(); 
        }

        public async Task InsertMetrics(MetricasCitaMedica metrics) {
             await _metapprepo.InsertAsync(metrics); 
        }

        public async Task UpdateMetrics(MetricasCitaMedica metrics) {
            await _metapprepo.UpdateAsync(metrics); 
        }


        public async Task<CitaMedica> GetMedicalAppointmentWithAppointmentByIdAsync(int id) {
            return await _medapprepo.GetMedicalAppointmentByWithAppointmentIdAsync(id); 
        
        }
        /*
        public async Task<EstadoCitaMedica> GetMedAppointmentStatusAsync(int id) {
            return await _appostatusrep.GetByKeyAsync(id); 
        }
        */
        public async Task<CentroMedico> GetMedCenterByKeyAsync(int id) {
            return await _medcenrepo.GetByKeyAsync(id); 
        }

        public async Task UpdateAsync(PoseeReceta prescription) {
            await _havepresc.UpdateAsync(prescription);
        }

        public async Task<IEnumerable<RecetaMedica>> GetDrugsByConditionAsync(string drugname) {
            return await _drugrepo.GetByConditionAsync(e => e.NombreReceta == drugname); 
        }

        public async Task<IEnumerable<RecetaMedica>> GetDrugsByFilterAsync(string filter) {
            string drug = "%" + filter + "%";
            return await _drugrepo.GetDrugsByFilterAsync(drug); 
        }

        public async Task<PoseeReceta> UpdatePrescriptionDosis(int idMedicalPrescription, int idMedicalAppointment, string dosis) {
            return await _havepresc.UpdatePescription(idMedicalPrescription, idMedicalAppointment, dosis); 
        
        }

        public async Task<PoseeReceta> GetDrugByConditionAsync(int drug_id, int appointmentId) {

            return await _havepresc.GetPrescriptionByDrugId(drug_id, appointmentId); 
        }

        public async Task<CitaMedica> GetMedicalAppointmentByAppointmentId(int id) {
            return await _medapprepo.GetByAppointmentId(id); 
        }

        public async Task<CitaMedica> GetMedicalAppointmentByKeyAsync(int id) {
            return await _medapprepo.GetByKeyAsync(id); 
        }

        public async Task<CitaMedica> GetMedicalAppointmentWithAppointmentByKeyAsync(int id) {
            return await _medapprepo.GetMedicalAppointmentWithAppointmentByKeyAsync(id); 
        }

        public async Task<Cita> GetAppointmentByKeyAsync(int id) {
            return await _appointmentRepository.GetAppointmentWithRecordNPatientByKeyAsync(id); 
        }


        public async Task<PoseeReceta> InsertPrescription(int idMedicalPrescription, int idMedicalAppointment) {
            PoseeReceta temp = new PoseeReceta()
            {
                IdRecetaMedica = idMedicalPrescription,
                IdCitaMedica = idMedicalAppointment,
                Dosis = ""
            };

            return await _havepresc.InsertAsync(temp); 

        }

        public async Task<IEnumerable<RecetaMedica>> GetDrugsAsync() {
            return await _drugrepo.GetAllAsync(); 
        }

        public async Task<IEnumerable<PoseeReceta>> GetPrescriptionsByAppointmentId(int id) {

            //return await _havepresc.GetByConditionAsync(e => e.IdCitaMedica == id); 

            return await _havepresc.GetPrescriptionByAppointmentId(id); 
        }


        public async Task<IEnumerable<TipoAccion>> GetActionTypesAsync(bool isIncident = true)
        {
            return await _actionTypeRepo.GetByConditionAsync(a => a.EsDeIncidente == true);
        }

        public async Task<IEnumerable<TipoAccion>> GetActionsTypesMedicalAppointmentAsync(bool isMedAppointment = true) {
            return await _actionTypeRepo.GetByConditionAsync(a => a.EsDeCitaMedica == isMedAppointment); 
        }

        public async Task<Expediente> AssignMedicalRecordAsync(int appointmentId, Paciente patient)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
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

        public async Task<Cita> GetLastAppointmentDateAsync(int id)
        {
            return await _appointmentRepository.getLatestAppointmentByRecordId(id);
        }

        public async Task<CitaMedica> InsertMedicalAppointmentAsync(CitaMedica model) 
        {
            return await _medapprepo.InsertAsync(model);
        }

        public async Task<Cita> InsertAppointmentAsync(Cita model)
        {
            return await _appointmentRepository.InsertAsync(model);   
        }

    }
        [MetadataType(typeof(AppointmentServiceAuthorization))]
        public partial class AppointmentService { }
}
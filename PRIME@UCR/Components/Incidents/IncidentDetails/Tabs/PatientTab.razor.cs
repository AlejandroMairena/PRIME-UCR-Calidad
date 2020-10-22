using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class PatientTab
    {
        [Inject] private IPatientService PatientService { get; set; }
        [Inject] private IPersonService PersonService { get; set; }
        [Inject] private IMedicalRecordService MedicalRecordService { get; set; }
        [Parameter] public Expediente Expediente { get; set; }
        [Parameter] public EventCallback<PatientModel> OnSave { get; set; }
        
        
        private PatientModel _model = new PatientModel();
        private bool _patientReady = false;
        private bool _isLoading = false;
        private EditContext _context;
        
        private async Task OnIdChange(string id)
        {
            _model.CedPaciente = id;
            if (_context.Validate())
            {
                // if valid
                var patient = await PatientService.GetPatientByIdAsync(_model.CedPaciente);
                if (patient != null)
                {
                    _model.Patient = patient;
                    _patientReady = true;
                }
                else // check for person
                {
                    var person = await PersonService.GetPersonByIdAsync(id);
                    if (person != null)
                    {
                        // warn: existing person, add them as patient?
                        // we assume that we always create patient 
                        patient = new Paciente
                        {
                            Cédula = person.Cédula,
                            FechaNacimiento = person.FechaNacimiento,
                            Nombre = person.Nombre,
                            PrimerApellido = person.PrimerApellido,
                            SegundoApellido = person.SegundoApellido,
                            Sexo = person.Sexo
                        };
                        _model.Patient = await PatientService.InsertPatientOnlyAsync(patient);
                        _patientReady = true;
                    }
                    else
                    {
                        _model.Patient = new Paciente
                        {
                            Cédula = _model.CedPaciente
                        };
                    }

                    if (_patientReady)
                    {
                        var result = await MedicalRecordService.GetByPatientIdAsync(_model.Patient.Cédula);
                        if (result == null)
                        {
                            var medicalRecord = new Expediente
                            {
                                CedulaPaciente = _model.Patient.Cédula
                            };
                            _model.Expediente =  await MedicalRecordService.CreateMedicalRecordAsync(medicalRecord);
                        }
                    }
                }
            }
            else
            {
                _model.Patient = null;
            }
        }

        protected override void OnInitialized()
        {
           _model.Expediente = Expediente;
           _context = new EditContext(_model);
        }

        private async Task AssignRecord()
        {
            Console.WriteLine(_model.CedPaciente);
        }
    }
}
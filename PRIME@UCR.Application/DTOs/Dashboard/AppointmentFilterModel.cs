using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.DTOs.Dashboard
{
    public class AppointmentFilterModel
    {
        public AppointmentFilterModel()
        {
            // final
            Hospital = new MedicalCenterLocationModel();
            PatientModel = new PatientModel();
            // selected
            _selectedHospital = new MedicalCenterLocationModel();
            _selectedPatientModel = new PatientModel();
        }

        // final
        public MedicalCenterLocationModel Hospital { get; set; }
        public PatientModel PatientModel { get; set; }

        // selected
        public MedicalCenterLocationModel _selectedHospital { get; set; }
        public PatientModel _selectedPatientModel { get; set; }

        public bool ButtonEnabled { get; set; }
    }
}

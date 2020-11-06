using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.DTOs.Dashboard
{
    public class FilterModel
    {
        public FilterModel()
        {
        }

        public DateTime? InitialDateFilter { get; set; }
        public DateTime? FinalDateFilter { get; set; }

        public CentroMedico MedicalCenterDestination { get; set; }

        public HouseholdModel HouseholdOriginFilter { get; set; }
        public InternationalModel InternationalOriginFilter { get; set; }
        public MedicalCenterLocationModel MedicalCenterOriginFilter { get; set; }

    }
}

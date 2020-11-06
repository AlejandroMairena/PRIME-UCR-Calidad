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
            MedicalCenterDestination = new MedicalCenterLocationModel();
            OriginFilter = new OriginModel();
            HouseholdOriginFilter = new HouseholdModel();
            InternationalOriginFilter = new InternationalModel();
            MedicalCenterOriginFilter = new MedicalCenterLocationModel();
        }

        public DateTime? InitialDateFilter { get; set; }
        public DateTime? FinalDateFilter { get; set; }

        public MedicalCenterLocationModel MedicalCenterDestination { get; set; }
        public OriginModel OriginFilter { get; set; }
        public HouseholdModel HouseholdOriginFilter { get; set; }
        public InternationalModel InternationalOriginFilter { get; set; }
        public MedicalCenterLocationModel MedicalCenterOriginFilter { get; set; }

    }
}

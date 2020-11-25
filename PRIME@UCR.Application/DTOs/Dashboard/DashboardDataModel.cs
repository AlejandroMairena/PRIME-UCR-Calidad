using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.Dashboard
{
    public class DashboardDataModel
    {
        public DashboardDataModel()
        {
            incidentsData = new List<Incidente>();
            medicalCenters = new List<CentroMedico>();
            modalities = new List<Modalidad>();
            states = new List<Estado>();
            districts = new List<Distrito>();
            isReadyToShowFilters = false;
        }

        public List<Incidente> incidentsData { get; set; }

        public List<CentroMedico> medicalCenters { get; set; }

        public List<Modalidad> modalities { get; set; }
        
        public List<Estado> states { get; set; }
        
        public List<Pais> countries { get; set; }
        
        public List<Distrito> districts { get; set; }

        public bool isReadyToShowFilters { get; set; }
    }
}

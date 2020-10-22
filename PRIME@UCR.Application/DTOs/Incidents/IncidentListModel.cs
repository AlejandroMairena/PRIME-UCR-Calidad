using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.Incidents
{
    public class IncidentListModel
    {
        public string Codigo { get; set; }

        public DateTime FechaHoraRegistro { get; set; }

        public string Estado { get; set; }
        public string Modalidad { get; set; }

        public Ubicacion Origen { get; set; }

        public CentroMedico Destino { get; set; }
        public int? IdDestino { get; set; }

    }
}

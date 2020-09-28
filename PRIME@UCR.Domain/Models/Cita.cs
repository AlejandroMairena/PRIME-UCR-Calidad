using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class Cita
    {
        public string IDCita { get; set; }

        public DateTime FechaHoraCreacion { get; set; }

        public DateTime FechaHoraEstimada { get; set; }

        public List<Acciones> Acciones { get; set; }

        public List<CitaMedica> CitasMedicas { get; set; }

        public List<Metricas> Metricas { get; set; }

    }
}

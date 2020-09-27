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


        //PENDIENTES. 
        //public List<CitaMedica>

        //public List<Metricas>

    }
}

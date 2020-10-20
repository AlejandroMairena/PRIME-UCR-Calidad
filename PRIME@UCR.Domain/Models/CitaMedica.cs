using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class CitaMedica
    {
        [Key]
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public string IDCita { get; set; }  //fk-cita
        public string CedMedicoAsignado { get; set; } //fk-medico
        public decimal NumExpediente { get; set; } //fk-expediente. 

        public Cita Cita { get; set; }

        public Médico Medico { get; set; }

        public Expediente Expediente { get; set; }

    }
}

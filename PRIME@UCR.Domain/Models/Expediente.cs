using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class Expediente
    {
        [Key]
        public int NumExpediente { get; set; }
        public string Clinica { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string CedPaciente { get; set; } //fk-paciente
        public string CedMedicoDuenno { get; set; } //fk-medico

        public Médico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public List<CitaMedica> CitasMedicas { get; set; }
    }
}

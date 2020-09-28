using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class Expediente
    {
        public int NumExpediente { get; set; }
        public string Clinica { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string CedPaciente { get; set; } //fk-paciente
        public string CedMedicoDuenno { get; set; } //fk-medico

        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public List<CitaMedica> CitasMedicas { get; set; }
    }
}

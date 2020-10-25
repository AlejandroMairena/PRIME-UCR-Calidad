using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models.MedicalRecords
{
    public class Expediente
    {
        public int Id { get; set; }
        //public DateTime FechaHoraCreacion { get; set; }
        public string CedulaPaciente { get; set; } //fk-paciente
        public string CedulaMedicoDuenno { get; set; } //fk-medico

        public Médico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public List<Cita> Citas { get; set; }
    }
}

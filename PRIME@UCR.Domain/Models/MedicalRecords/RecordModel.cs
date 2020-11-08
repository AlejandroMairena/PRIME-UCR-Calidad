using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.MedicalRecords
{
    public class RecordModel
    {
        public Expediente Expediente { get; set; }

        public Persona Persona { get; set; }

        public Paciente Paciente { get; set; }

        public string CedPaciente { get; set; }

        public string CedMedicoDuenno { get; set; }

        public string Clinica { get; set; }

    }
}

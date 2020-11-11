using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.MedicalRecords
{
    public class DateIncidentModel
    {
        public Cita date { get; set; }

        public Incidente incident { get; set; }

        public string type { get; set; } = "incidente"; 
    }
}

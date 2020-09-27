using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class Medico
    {
        public List<CitaMedica> CitasMedicas { get; set; }

        public List<Expediente> Expedientes { get; set; }
    }
}

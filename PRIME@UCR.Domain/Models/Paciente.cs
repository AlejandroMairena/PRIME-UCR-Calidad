using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
   public class Paciente
    {
        public string Cedula { get; set; }
        public Expediente Expediente { get; set; }

    }
}

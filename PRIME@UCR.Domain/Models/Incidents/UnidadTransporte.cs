using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.Incidents
{
    class UnidadTransporte
    {
        public string Estado { get; set; }
        public string Matricula { get; set; }
        public ModalidadIncidente Modalidad { get; set; }
    }
}

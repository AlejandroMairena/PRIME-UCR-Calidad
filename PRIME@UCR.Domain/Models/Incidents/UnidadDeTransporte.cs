using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.Incidents
{
    public class UnidadDeTransporte
    {
        public UnidadDeTransporte() {
            Incidentes = new List<Incidente>();
        }
        public string Matricula { get; set; }
        public string Estado { get; set; }
        public string Modalidad { get; set; }
        public Modalidad ModalidadTrasporte { get; set; }
        public List<Incidente> Incidentes { get; private set; }
    }
}

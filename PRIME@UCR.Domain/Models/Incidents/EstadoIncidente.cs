using System;

namespace PRIME_UCR.Domain.Models.Incidents
{
    public class EstadoIncidente
    {
        public Estado Estado { get; set; }
        public string NombreEstado { get; set; }
        
        public Incidente Incidente { get; set; }
        public string CodigoIncidente { get; set; }
        
        public bool Activo { get; set; }
        public DateTime FechaModificado { get; set; }
    }
}
using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models.Incidents
{
    public class Estado
    {
        public Estado()
        {
            EstadoIncidentes = new List<EstadoIncidente>();
        }

        public string Nombre { get; set; }
        public List<EstadoIncidente> EstadoIncidentes { get; private set; }
    }
}
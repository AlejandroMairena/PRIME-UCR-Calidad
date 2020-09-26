using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class Pais
    {
        public Pais()
        {
            Provincias = new List<Provincia>();
            PaisUbicaciones = new List<PaisUbicacion>();
        }

        public string Nombre { get; set; }
        public List<Provincia> Provincias { get; private set; }
        public List<PaisUbicacion> PaisUbicaciones { get; private set; }
    }
}
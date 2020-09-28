using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class Pais
    {
        public const string DefaultCountry = "Costa Rica";
        
        public Pais()
        {
            Provincias = new List<Provincia>();
            PaisUbicaciones = new List<Internacional>();
        }

        public string Nombre { get; set; }
        public List<Provincia> Provincias { get; private set; }
        public List<Internacional> PaisUbicaciones { get; private set; }
    }
}
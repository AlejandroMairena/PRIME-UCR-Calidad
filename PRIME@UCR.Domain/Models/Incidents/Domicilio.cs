using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class Domicilio
    {
        public Domicilio() {
            UbicacionIncidentes = new List<DomicilioUbicacion>(); 
        }
        public int Id {get; set;}
        public string Direccion { get; set; }
        public int DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public List<DomicilioUbicacion> UbicacionIncidentes { get; private set; }
    }
}
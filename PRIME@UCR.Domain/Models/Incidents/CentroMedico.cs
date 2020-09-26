using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class CentroMedico
    {
        public CentroMedico() {
            UbicacionIncidentes = new List<CentroUbicacion>();
        }
        public int Id { get; set; }
        public int UbicadoEn { get; set; }
        public Distrito Distrito { get; set; }
        public double Longitud { get; set; }        
        public double Latitud { get; set; }
        public string Nombre { get; set; }
        public List<CentroUbicacion> UbicacionIncidentes { get; set; }
    }
}
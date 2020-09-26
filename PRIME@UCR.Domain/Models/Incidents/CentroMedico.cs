using PRIME_UCR.Domain.Models;
using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class CentroMedico
    {
        public CentroMedico()
        {
            CentroUbicaciones = new List<CentroUbicacion>(); 
        }
        public string Nombre { get; set; }        
        public double Longitud { get; set; }        
        public double Latitud { get; set; }
        public int Id { get; set; }
        public int DistritoId { get; set; }
        public List<CentroUbicacion> CentroUbicaciones { get; private set; }
        public Distrito Distrito { get; set; }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class CentroUbicacion
    {
        public CentroUbicacion()
        {
            CentroUbicaciones = new List<CentroUbicacion>(); 
        }
        public double Longitud { get; set; }        
        public double Latitud { get; set; }
        public int Id { get; set; }
        public List<CentroUbicacion> CentroUbicaciones { get; private set; }
    }
}

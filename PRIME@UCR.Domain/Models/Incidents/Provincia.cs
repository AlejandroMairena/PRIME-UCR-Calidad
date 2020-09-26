using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class Provincia
    {
        public Provincia() { 
            Cantones = new List<Canton>();
        }
        public string Nombre { get; set; }
        public string NombrePais { get; set; }
        public Pais Pais { get; set; }
        public List<Canton> Cantones { get; private set; }
    }
}
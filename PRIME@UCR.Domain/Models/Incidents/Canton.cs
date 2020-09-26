using PRIME_UCR.Domain.Models;
using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class Canton
    {
        public Canton(){
            Distritos = new List<Distrito>(); 
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreProvincia { get; set; }
        public Provincia Provincia { get; set; }
        public List<Distrito> Distritos { get; private set; }
    }
}
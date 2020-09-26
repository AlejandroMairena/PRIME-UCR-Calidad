using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class Distrito
    {
        public Distrito() {
            CentrosMedicos = new List<CentroMedico>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCanton { get; set; }
        public Canton Canton { get; set; }
        public List<CentroMedico> CentrosMedicos { get; set;}
    }
}
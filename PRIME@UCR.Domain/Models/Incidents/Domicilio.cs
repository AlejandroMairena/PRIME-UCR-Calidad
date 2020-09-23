namespace PRIME_UCR.Domain.Models
{
    public class Domicilio
    {
        public string DireccionExacta { get; set; }
        public Distrito Distrito { get; set; }
        public double Longitud { get; set; }        
        public double Latitud { get; set; }
        public int Id { get; set; }
    }
}
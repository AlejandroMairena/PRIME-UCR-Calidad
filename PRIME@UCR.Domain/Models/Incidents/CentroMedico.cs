namespace PRIME_UCR.Domain.Models
{
    public class CentroMedico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Longitud { get; set; }        
        public double Latitud { get; set; }
        public Distrito Distrito { get; set; }
    }
}
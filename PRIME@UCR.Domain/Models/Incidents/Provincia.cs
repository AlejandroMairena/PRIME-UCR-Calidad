namespace PRIME_UCR.Domain.Models
{
    public class Provincia
    {
        public string Nombre { get; set; }
        public string NombrePais { get; set; }
        public Pais Pais { get; set; }
    }
}
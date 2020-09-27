namespace PRIME_UCR.Domain.Models
{
    public class Internacional : Ubicacion
    {
        public int Id { get; set; }
        public string NombrePais { get; set; }
        public Pais Pais { get; set; }

    }
}
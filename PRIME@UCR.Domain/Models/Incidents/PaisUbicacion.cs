namespace PRIME_UCR.Domain.Models
{
    public class PaisUbicacion
    {
        public int Id { get; set; }
        public string NombrePais { get; set; }
        public Pais Pais { get; set; }
        public int UbicacionId {get; set; }
        public Ubicacion Ubicacion { get; set; }

    }
}
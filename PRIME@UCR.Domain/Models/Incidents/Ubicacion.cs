namespace PRIME_UCR.Domain.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string CedulaDeMedico { get; set; }
        public DomicilioUbicacion DomicilioUbicacion { get; set; }
        public CentroUbicacion CentroUbicacion { get; set; }
        public PaisUbicacion PaisUbicacion { get; set; }
        public Incidente IncidenteOrigen { get; set; }
        public Incidente IncidenteDestino { get; set; }
        //public Medico Medico { get; set; } //not implemented
        

    }
}
        
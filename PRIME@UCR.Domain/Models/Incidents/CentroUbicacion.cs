using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class CentroUbicacion
    {
        public int Id { get; set; }
        public int CentroMedicoId { get; set; }
        public CentroMedico CentroMedico { get; set; }
        public int UbicacionId { get; set; }
        public Ubicacion Ubicacion { get; set; }

    }
}

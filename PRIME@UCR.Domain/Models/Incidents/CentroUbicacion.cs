using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class CentroUbicacion : Ubicacion
    {
        public int CentroMedicoId { get; set; }
        public CentroMedico CentroMedico { get; set; }
    }
}

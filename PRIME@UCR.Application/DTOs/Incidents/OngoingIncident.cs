using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Application.DTOs.Incidents
{
    // represents basic data relevant to an incident while it is ongoing
    public class OngoingIncident
    {
        public Incidente Incident { get; set; }
        public UnidadDeTransporte TransportUnit { get; set; }
        public Ubicacion Origin { get; set; }
        public Ubicacion Destination { get; set; }
        public Modalidad UnitType { get; set; }
    }
}

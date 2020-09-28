using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Domain.Constants
{
    public static class IncidentStates
    {
        // TODO: fill with known states
        public static readonly Estado InCreationProcess = new Estado {Nombre = "En proceso de creación"};
        public static readonly Estado Created = new Estado {Nombre = "Creado"};
    }
}
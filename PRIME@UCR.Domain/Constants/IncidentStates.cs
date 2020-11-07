﻿using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Domain.Constants
{
    public static class IncidentStates
    {
        // TODO: fill with known states
        public static readonly Estado InCreationProcess = new Estado {Nombre = "En proceso de creación"};
        public static readonly Estado Created = new Estado {Nombre = "Creado"};
        public static readonly Estado Rejected = new Estado {Nombre = "Rechazado"};
        public static readonly Estado Approved = new Estado {Nombre = "Aceptado"};
        public static readonly Estado Assigned = new Estado { Nombre = "Asignado" };
        public static readonly Estado Preparing = new Estado { Nombre = "En preparación" };
        public static readonly Estado InOriginRoute = new Estado { Nombre = "En ruta a origen" };
        public static readonly Estado PatientInOrigin = new Estado { Nombre = "Paciente recolectado en origen" };
        public static readonly Estado InRoute = new Estado { Nombre = "En traslado" };
        public static readonly Estado Delivered = new Estado { Nombre = "Entregado" };
        public static readonly Estado Reactivated = new Estado { Nombre = "Reactivado" };
        public static readonly Estado Done = new Estado { Nombre = "Finalizado" };
    }
}
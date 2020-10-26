﻿using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRIME_UCR.Domain.Constants;

namespace PRIME_UCR.Domain.Models
{
    public class Incidente
    {
        public Incidente()
        {
            EstadoIncidentes = new List<EstadoIncidente>();
        }

        public string Codigo { get; set; }
        public List<EstadoIncidente> EstadoIncidentes { get; private set; }
        public int? IdOrigen { get; set; }
        public Ubicacion Origen {get; set;}
        public int? IdDestino { get; set; }
        public Ubicacion Destino {get; set;}
        public string TipoModalidad { get; set; }
        public Modalidad Modalidad {get; set;}
        public string MatriculaTrans { get; set; }
        public UnidadDeTransporte UnidadDeTransporte {get; set;}
        public int CodigoCita { get; set; }
        public Cita Cita { get; set; }
        public string CedulaAdmin { set; get; }
        public string CedulaTecnicoCoordinador { get; set; }
        public string CedulaTecnicoRevisor { get; set; }

        public bool IsCompleted()
        {
            return IdOrigen != null &&
                   IdDestino != null &&
                   Cita?.IdExpediente != null;
        }

        public bool IsModifiable(Estado currentState)
        {
            return currentState.Nombre == IncidentStates.InCreationProcess.Nombre ||
                   currentState.Nombre == IncidentStates.Created.Nombre;
        }
    }
}
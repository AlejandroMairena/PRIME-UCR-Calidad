using PRIME_UCR.Domain.Models.Incidents;
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
        public DateTime FechaHoraEstimada { get; set; } // temp
        public DateTime FechaHoraRegistro { get; set; }
        public int? IdOrigen { get; set; }
        public Ubicacion Origen {get; set;}
        public int? IdDestino { get; set; }
        public Ubicacion Destino {get; set;}
        public string TipoModalidad { get; set; }
        public Modalidad Modalidad {get; set;}
        public string MatriculaTrans { get; set; }
        public UnidadDeTransporte UnidadDeTransporte {get; set;}
        
        // public int? CodigoCita { get; set; }
        // public int? IdEspecialista { get; set; }
        //public Especialista Especialista {get; set;}
        // public int? CedulaAdmin { set; get; }
        //public Administrador Administrador {get; set;}
        // public int? CedulaTecnicoCoordinador { get; set; }
        //public TecnicoCoordinador TecnicoCordinador {get; set;}
        // public int? CedulaTecnicoRevisor { get; set; }
        //public TecnicoRevisor {get; set;}

        public bool IsCompleted()
        {
            return IdOrigen != null &&
                   IdDestino != null;
        }

        public bool IsModifiable(Estado currentState)
        {
            return currentState.Nombre == IncidentStates.InCreationProcess.Nombre ||
                   currentState.Nombre == IncidentStates.Created.Nombre;
        }
    }
}
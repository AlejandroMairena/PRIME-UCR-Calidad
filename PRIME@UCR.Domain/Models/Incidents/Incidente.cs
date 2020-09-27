using PRIME_UCR.Domain.Models.Incidents;
using System;

namespace PRIME_UCR.Domain.Models
{
    public class Incidente
    {
        public string Codigo { get; set; }
        public string MatriculaTrans { get; set; }
        //public Unidad_De_Transporte Unidad_de_transporte { get; set; }
        public string Estado { get; set; }
        public int IdEspecialista { get; set; }
        //public Especialista Especialista {get; set;}
        public string Id { get; set; }
        public int CedulaAdmin { set; get; }
        //public Administrador Administrador {get; set;}
        public int CedulaTecnicoCoordinador { get; set; }
        //public TecnicoCoordinador TecnicoCordinador {get; set;}
        public int CedulaTecnicoRevisor { get; set; }
        //public TecnicoRevisor {get; set;}
        public int CodigoCita { get; set; }
        public DateTime FechaHoraEstimada { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public int IdOrigen { get; set; }
        public Ubicacion Origen {get; set;}
        public int IdDestino { get; set; }
        public Ubicacion Destino {get; set;}
        public string TipoModalidad { get; set; }
        public Modalidad Modalidad {get; set;}
        public UnidadDeTransporte UnidadDeTransporte {get; set;}
    }
}
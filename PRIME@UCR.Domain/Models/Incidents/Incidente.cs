﻿namespace PRIME_UCR.Domain.Models
{
    public class Incidente
    {
        public Ubicacion Origen { get; set; } 
        public Ubicacion Destino { get; set; }
        public string Estado { get; set; }
        // public UnidadTransporte UniTransporte { get; set; }
        // public EspecialistaMedico Especialista {}
        // public Administrador Administrador {}
        // public TecnicoCoordinador TecnicoCoordinador {}
        // public TecnicoRevisor  TecnicoRevisor {}
        // public Cita Cita {}
        public ModalidadIncidente Modalidad { get; set; }


    }
}
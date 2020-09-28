﻿using System;
using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class CentroMedico
    {

        public CentroMedico() {
            UbicacionIncidentes = new List<CentroUbicacion>();
        }
        public int Id { get; set; }
        public int UbicadoEn { get; set; }
        public Distrito Distrito { get; set; }
        public double Longitud { get; set; }        
        public double Latitud { get; set; }
        public string Nombre { get; set; }
        public List<CentroUbicacion> UbicacionIncidentes { get; set; }
        
        protected bool Equals(CentroMedico other)
        {
            return Id == other.Id &&
                   UbicadoEn == other.UbicadoEn &&
                   Longitud.Equals(other.Longitud) &&
                   Latitud.Equals(other.Latitud) &&
                   Nombre == other.Nombre;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CentroMedico) obj);
        }
    }
}
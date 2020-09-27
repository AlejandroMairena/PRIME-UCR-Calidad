﻿using PRIME_UCR.Domain.Models;
using System.Collections.Generic;

namespace PRIME_UCR.Domain.Models
{
    public class Distrito
    {
        public Distrito()
        {
            CentroMedicos = new List<CentroMedico>();
            Domicilios = new List<Domicilio>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CantonId { get; set; }
        public Canton Canton { get; set; }
        public List<CentroMedico> CentroMedicos { get; private set; }
        public List<Domicilio> Domicilios { get; private set; }

    }
}
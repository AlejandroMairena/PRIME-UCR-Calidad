﻿using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class Metricas
    {
        [Key]
        public string ID { get; set; }
        public string IDCita { get; set; } //fk-cita
        public decimal Presion { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public decimal Temperatura { get; set; }

        public Cita Cita { get; set; }

        public Médico Medico { get; set; }

        public Expediente Expediente { get; set; }
    }
}

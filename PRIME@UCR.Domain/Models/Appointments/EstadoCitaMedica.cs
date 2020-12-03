﻿using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models.Appointments
{
    class EstadoCitaMedica
    {
        [Key]
        public int Codigo { get; set; }
        
        public string NombreEstado { get; set; }
    }
}

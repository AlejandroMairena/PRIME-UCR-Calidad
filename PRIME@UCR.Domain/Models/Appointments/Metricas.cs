using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public abstract class Metricas
    {
        public int Id { get; set; }
        public int CitaId { get; set; } //fk-cita

        public Cita Cita { get; set; }


    }
}

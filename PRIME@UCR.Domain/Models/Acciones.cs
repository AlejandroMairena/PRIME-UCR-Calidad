using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class Acciones
    {
        [Key]
        public string ID { get; set; }

        public string IDCita { get; set; }

        public string Tipo { get; set; }

        public string Descripcion { get; set; } 

        public Cita Cita { get; set; }

        public List<MultimediaContent> MultimediaContents { get; set; }
    }
}

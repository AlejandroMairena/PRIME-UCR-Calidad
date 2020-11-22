using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PRIME_UCR.Domain.Models.Appointments
{
    public class RecetaMedica
    {
        [Key]
        public int Codigo { get; set; }

        public string NombreMedicamento { get; set; }

        public List<PoseeReceta> Recetas { get; set; }
        
    }
}

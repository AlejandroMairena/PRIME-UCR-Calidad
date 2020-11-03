using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.MedicalRecords
{
    public class Alergia
    {
        public int Id { set; get; } //Llave parcial para identificar la alergia con respecto al expediente

        public string Nombre { set; get; }  //Nombre de la alergia

        public int IdExpediente { set; get; }  //Foreign key a Expediente

        public Expediente Expediente { set; get; }
    }
}

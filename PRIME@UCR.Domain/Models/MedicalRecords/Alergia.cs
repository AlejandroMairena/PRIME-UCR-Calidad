using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.MedicalRecords
{
    public class Alergia
    {
        public int Id { set; get; } //Llave parcial para identificar la alergia con respecto al expediente

        public int IdExpediente { set; get; }  //Foreign key a Expediente, parte de la primary key

        public int IdListaAlergia { set; get; }  //Foreign key a ListaAlergia, parte de la primary key

        public Expediente Expediente { set; get; }

        public ListaAlergia ListaAlergia { set; get; }
    }
}

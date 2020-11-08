using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PRIME_UCR.Domain.Models.MedicalRecords
{
    public class Antecedente
    {
        public int Id { get; set; }
        public int IdExpediente { get; set; }
        public int IdListaAntecedente { get; set; }
        public Expediente Expediente { get; set; }
        public ListaAntecedentes ListaAntecedentes { get; set; }
    }
}

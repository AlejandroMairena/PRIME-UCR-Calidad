using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.MedicalRecords
{
    public class ListaAntecedentes
    {
        public int Id { get; set; }
        public string NombreAntecdedente { get; set; }
        public List<Antecedente> Antecedente { get; set; }
    }
}

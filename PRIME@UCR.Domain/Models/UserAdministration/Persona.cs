using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    public class Persona
    {
        public string Cédula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public char Sexo { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
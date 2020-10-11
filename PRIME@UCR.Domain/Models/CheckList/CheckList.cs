using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class CheckList
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public string NombreImagen { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.CheckList
{
    public class Item
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public string NombreImagen { get; set; }
        public int IDSuperItem { get; set; }
        public int IDLista { get; set; }
        public CheckList Checklist { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Item i)
                return Id == i.Id &&
                       Nombre == i.Nombre &&
                       IDLista == i.IDLista;

            return false;
        }
    }
}

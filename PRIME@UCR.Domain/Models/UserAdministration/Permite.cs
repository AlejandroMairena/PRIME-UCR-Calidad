using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    public class Permite
    {
        public string IDPerfil { get; set; }
        public Perfil Perfil { get; set; }

        public int IDPermiso { get; set; }
        public Permiso Permiso { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    public class Pertenece
    {
        public string IDUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public string IDPerfil { get; set; }
        public Perfil Perfil { get; set; }
    }
}

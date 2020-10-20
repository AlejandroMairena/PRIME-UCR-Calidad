using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    public class TienePerfil
    {
        public string CedFuncionario { get; set; }
        public Funcionario Funcionario { get; set; }

        public string IDPerfil { get; set; }
        public Perfil Perfil { get; set; }
    }
}

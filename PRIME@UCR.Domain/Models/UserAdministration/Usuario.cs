using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    public class Usuario : IdentityUser 
    {
        public string CédulaPersona { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}

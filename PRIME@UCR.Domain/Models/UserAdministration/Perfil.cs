using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    /**
     * Class used to model table Perfil from database.
     */
    public class Perfil
    {
        /**
         * Function:        Initialize each of the list.
         */
        public Perfil()
        {
            Permisos = new List<Permiso>();
            Usuarios = new List<Usuario>();
            Funcionarios = new List<Funcionario>();
        }

        /*String to store the name of the profile*/
        public string NombrePerfil { get; set; }

        /*List of permissions for the profile*/
        public List<Permiso> Permisos { get; set; }

        /*List of users that have the profile*/
        public List<Usuario> Usuarios { get; set; }

        /*List of functionaries that have the profile*/
        public List<Funcionario> Funcionarios { get; set; }
    }
}

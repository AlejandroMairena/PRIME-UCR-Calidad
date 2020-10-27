using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    /**
     * Class used to model table Persona from database.
     */
    public class Persona
    {
        /**
         * Function:        Initialize each of the list.
         */
        public Persona()
        {
            NumerosTelefono = new List<NúmeroTeléfono>();
        }

        /*String that identify the person*/
        public string Cédula { get; set; }

        /*String that store the name of the person*/
        public string Nombre { get; set; }

        /*String that store the first last name of the person*/
        public string PrimerApellido { get; set; }

        /*String that store the second last name of the person*/
        public string? SegundoApellido { get; set; }

        /*Character that store the sex of the person*/
        public char? Sexo { get; set; }

        /*Variable that store the birth date of the person*/
        public DateTime? FechaNacimiento { get; set; }

        /*List of phone numbers of the person*/
        public List<NúmeroTeléfono> NumerosTelefono { get; set; }

        public string NombreCompleto { get { return String.Format("{0} {1} {2}", Nombre, PrimerApellido, SegundoApellido); } }
    }
}
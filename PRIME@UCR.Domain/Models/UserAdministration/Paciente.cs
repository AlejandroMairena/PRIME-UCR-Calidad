using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    /**
     * Class used to model table Paciente from database.
     */
    public class Paciente : Persona
    {
        /*Foreign key to indicate the identifier of the medical record of a patient*/
        public int NumExpediente { get; set; }

        /*Object to store the medical record of the patient*/
        public Expediente Expediente { get; set; }
    }
}

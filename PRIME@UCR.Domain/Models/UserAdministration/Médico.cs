﻿using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.UserAdministration
{
    /**
     * Class used to model table Médico from database.
     */
    public class Médico : Funcionario
    {
        /*List of medical appointments which the doctor has to have*/
        public List<CitaMedica> CitasMedicas { get; set; }

        /*List of medical records for which the doctor has access*/
        public List<Expediente> Expedientes { get; set; }
    }
}

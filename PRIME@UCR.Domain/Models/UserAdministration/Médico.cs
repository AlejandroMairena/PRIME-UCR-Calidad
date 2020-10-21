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
        protected bool Equals(Médico other)
        {
            return Cédula == other.Cédula;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Médico) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cédula);
        }

        public Médico()
        {
            MedicosyCentrosMedicos = new List<TrabajaEn>();
        }
        /*List of medical appointments which the doctor has to have*/
        public List<CitaMedica> CitasMedicas { get; set; }

        /*List of medical records for which the doctor has access*/
        public List<Expediente> Expedientes { get; set; }
        public List<CentroUbicacion> CentrosMedicos { get; private set; }

        public List<TrabajaEn> MedicosyCentrosMedicos { get; set; }

    }
}

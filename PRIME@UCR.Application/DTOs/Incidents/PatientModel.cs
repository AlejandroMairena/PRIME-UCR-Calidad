using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Dtos.Incidents
{
    public class PatientModel
    {  
       public Expediente Expediente { get; set;}
       public string CedPaciente { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.DTOs.CheckLists
{
    public class PdfModel
    {
        public IncidentDetailsModel Incident { get; set; }
        public IEnumerable<PadecimientosCronicos> ChronicConditions { get; set; }
    }
}

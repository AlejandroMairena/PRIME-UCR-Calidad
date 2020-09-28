using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using PRIME_UCR.Application.ValidationAttributes;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Application.Dtos.Incidents
{
    public class IncidentModel
    {
        [Required]
        public Modalidad Mode { get; set; }

        [Required]
        [FutureDate]
        public DateTime EstimatedDateOfTransfer { get; set; }

    }
}
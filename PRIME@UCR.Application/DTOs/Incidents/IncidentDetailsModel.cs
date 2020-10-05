using System;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Application.Dtos.Incidents
{
    public class IncidentDetailsModel
    {
        public IncidentDetailsModel(
            int id,
            string code,
            string mode,
            string currentState,
            bool completed,
            bool modifiable,
            DateTime registrationDate,
            DateTime estimatedDateOfTransfer)
        {
            Id = id;
            Code = code;
            Mode = mode;
            CurrentState = currentState;
            Completed = completed;
            Modifiable = modifiable;
            RegistrationDate = registrationDate;
            EstimatedDateOfTransfer = estimatedDateOfTransfer;
        }

        public int Id { get; }
        [Required]
        public string Code { get; }

        [Required]
        public string Mode { get; }
        
        [Required]
        public string CurrentState { get; }

        public bool Completed { get; }

        public bool Modifiable { get; }
        
        public DateTime RegistrationDate { get; }
        
        public DateTime EstimatedDateOfTransfer { get; }

        [Required]
        public Ubicacion Origin { get; set; }
        
        [Required]
        public Ubicacion Destination { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Dtos.Incidents
{
    public class IncidentDetailsModel
    {
        public IncidentDetailsModel(
            string code,
            string mode,
            string currentState,
            bool completed,
            bool modifiable,
            DateTime registrationDate,
            DateTime estimatedDateOfTransfer,
            string adminId)
        {
            Code = code;
            Mode = mode;
            CurrentState = currentState;
            Completed = completed;
            Modifiable = modifiable;
            RegistrationDate = registrationDate;
            EstimatedDateOfTransfer = estimatedDateOfTransfer;
            AdminId = adminId;
        }

        public string Code { get; }
        public string Mode { get; }
        public string CurrentState { get; }
        public string AdminId { get; }
        public bool Completed { get; }
        public bool Modifiable { get; }
        public DateTime RegistrationDate { get; }
        public DateTime EstimatedDateOfTransfer { get; }
        public Ubicacion Origin { get; set; }
        public Ubicacion Destination { get; set; }
        public Expediente Expediente { get; set; }

    }
}
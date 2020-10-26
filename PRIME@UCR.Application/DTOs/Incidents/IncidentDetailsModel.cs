using System;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

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
            string adminId,
            string transportUnitId)
        {
            Code = code;
            Mode = mode;
            CurrentState = currentState;
            Completed = completed;
            Modifiable = modifiable;
            RegistrationDate = registrationDate;
            EstimatedDateOfTransfer = estimatedDateOfTransfer;
            AdminId = adminId;
            TransportUnitId = transportUnitId;
        }

        public string Code { get; set; }
        public string Mode { get; set; }
        public string CurrentState { get; set; }
        public string AdminId { get; set; }
        public bool Completed { get; set; }
        public bool Modifiable { get; set; }
        public string TransportUnitId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EstimatedDateOfTransfer { get; set; }
        public Ubicacion Origin { get; set; }
        public Ubicacion Destination { get; set; }
        public UnidadDeTransporte TransportUnit { get; set; }
    }
}
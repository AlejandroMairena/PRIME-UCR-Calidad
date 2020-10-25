using System;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Dtos.Incidents
{
    public class IncidentDetailsModel
    {
        public string Code { get; set; }
        public int AppointmentId { get; set; }
        public string Mode { get; set; }
        public string CurrentState { get; set; }
        public string AdminId { get; set; }
        public bool Completed { get; set; }
        public bool Modifiable { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EstimatedDateOfTransfer { get; set; }
        public Ubicacion Origin { get; set; }
        public Ubicacion Destination { get; set; }
        public string TransportUnitId { get; set; }
        public UnidadDeTransporte TransportUnit { get; set; }
        public Expediente MedicalRecord { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Application.DTOs.Incidents
{
    public class AssignmentModel
    {
        [Required]
        public UnidadDeTransporte TransportUnit { get; set; }
    }
}

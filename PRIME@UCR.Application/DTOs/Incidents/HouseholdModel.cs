using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Dtos.Incidents
{
    public class HouseholdModel
    {
        public Distrito District { get; set; }
        public Canton Canton { get; set; }
        public Provincia Province { get; set; }
        public string Address { get; set; } 
        public double Longitude { get; set; } 
        public double Latitude { get; set; } 
    }
}
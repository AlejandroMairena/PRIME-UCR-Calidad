using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Dtos
{
    public class LocationModel
    {
        public Pais Country { get; set; }
        public Provincia Province { get; set; }
        public Canton Canton { get; set; }
        public Distrito District { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.CheckLists
{
    public class InstanciaItemPadre
    {
        public int ItemId { get; set; }
        public int PlantillaId { get; set; }
        public string IncidentCod { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFinal { get; set; }
    }
}

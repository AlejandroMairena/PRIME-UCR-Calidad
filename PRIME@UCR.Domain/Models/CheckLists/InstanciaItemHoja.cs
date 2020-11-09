using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.CheckLists
{
    public class InstanciaItemHoja : InstanciaItem
    {
        public int ItemIdPadre { get; set; }
        public int PlantillaIdPadre { get; set; }
        public string IncidentCodPadre { get; set; }
        public DateTime? FechaHora { get; set; }
        public InstanciaItemPadre ItemPadre { get; set; }
    }
}

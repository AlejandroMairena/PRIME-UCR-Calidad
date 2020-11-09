using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.CheckLists
{
    public abstract class InstanciaItem
    {
        public int ItemId { get; set; }
        public int PlantillaId { get; set; }
        public string IncidentCod { get; set; }
        public bool Completado { get; set; }
    }
}

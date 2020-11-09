using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.CheckLists
{
    public class InstanciaItemPadre : InstanciaItem
    {
        public DateTime? FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFinal { get; set; }
        public List<InstanciaItemHoja> ItemsHoja { get; set; }
    }
}

using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class StatePanel
    {
        public List<string> IncidentStatesList = new List<string> {
            "Creando",
            "Creado",
            "Rechazado",
            "Aceptado",
            "Asignado",
            "Preparación",
            "Hacia origen",
            "Colecta",
            "Traslado",
            "Entregado",
            "Reactivado",
            "Finalizado"
        };

        MatButton Button2;
        BaseMatMenu Menu2;

        public void OnClick(MouseEventArgs e)
        {
            this.Menu2.OpenAsync(Button2.Ref);
        }
    }
}


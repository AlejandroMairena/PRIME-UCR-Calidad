using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class StatePanel
    {

        public string nextState;

        public List<string> PendingTasks = new List<string>();

        [Parameter]
        public IncidentDetailsModel Incident { get; set; }

        [Inject]
        public IIncidentService IncidentService { get; set; }

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

        protected override async Task OnInitializedAsync()
        {
            nextState = (await IncidentService.GetNextIncidentState(Incident.Code)).ToString();
            PendingTasks.Add("Daniel");
            PendingTasks.Add(" ");
            PendingTasks.Add("Esteban");
        }

        MatButton Button2;
        BaseMatMenu Menu2;

        public void OnClick(MouseEventArgs e)
        {
            this.Menu2.OpenAsync(Button2.Ref);
        }

        public void FormatPendingTasks()
        {
            for(int index = 0; index < PendingTasks.Count; ++index)
            {

            }
        }
    }
}


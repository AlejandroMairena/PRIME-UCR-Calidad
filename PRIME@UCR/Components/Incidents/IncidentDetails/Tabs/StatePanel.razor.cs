using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;
using Radzen;
using RepoDb.Exceptions;
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

        [Parameter] 
        public EventCallback OnSave { get; set; }


        [Inject]
        public IIncidentService IncidentService { get; set; }

        [Parameter]
        public Persona CurrentUser { get; set; }


        private int currentStateIndex;
        MatButton Button2;
        BaseMatMenu Menu2;
        public List<Tuple<string, string>> IncidentStatesList = new List<Tuple<string, string>> {
            Tuple.Create(IncidentStates.InCreationProcess.Nombre ,"Iniciado"),
            Tuple.Create(IncidentStates.Created.Nombre,"Creado"),
            Tuple.Create(IncidentStates.Rejected.Nombre, "Rechazado"),
            Tuple.Create(IncidentStates.Approved.Nombre, "Aprobado"),
            Tuple.Create(IncidentStates.Assigned.Nombre, "Asignado"),
            Tuple.Create(IncidentStates.Preparing.Nombre, "Preparación"),
            Tuple.Create(IncidentStates.InOriginRoute.Nombre, "Hacia origen"),
            Tuple.Create(IncidentStates.PatientInOrigin.Nombre, "Colecta"),
            Tuple.Create(IncidentStates.InRoute.Nombre, "Traslado"),
            Tuple.Create(IncidentStates.Delivered.Nombre, "Entregado"),
            Tuple.Create(IncidentStates.Reactivated.Nombre, "Reactivado"),
            Tuple.Create(IncidentStates.Done.Nombre, "Finalizado")
        };

        protected override async Task OnInitializedAsync()
        {
            currentStateIndex = IncidentStatesList.FindIndex(i => i.Item1 == Incident.CurrentState);
            nextState = (await IncidentService.GetNextIncidentState(Incident.Code)).ToString();
            PendingTasks = await IncidentService.GetPendingTasksAsync(Incident, nextState);
        }

        public string setStateColor(int index)
        {
            string className = "";
            if (index < currentStateIndex)
            {
                className = "bg-primary border  border-light";
            } else if(index > currentStateIndex)
            {
                className = "bg-secondary border border-light";
            }
            else
            {
                className = "bg-current-state border border-light";
            }
            return className;
        }

        public string setProgressIndicator(int index)
        {
            return index == currentStateIndex? "bi bi-caret-down-fill indicator-color" : "bi bi-caret-down-fill invisible";
        }


        public void OnClick(MouseEventArgs e)
        {
            this.Menu2.OpenAsync(Button2.Ref);
        }

        private async Task Approve()
        {
            await IncidentService
                .ApproveIncidentAsync(Incident.Code, CurrentUser.Cédula);
            await OnSave.InvokeAsync(null);
        }

        private async Task Reject()
        {
            await IncidentService
                .RejectIncidentAsync(Incident.Code, CurrentUser.Cédula);
            await OnSave.InvokeAsync(null);
        }

        private async Task ChangeState()
        {
            await IncidentService.ChangeState(Incident.Code, nextState);
            await OnSave.InvokeAsync(null);
        }
    }
}


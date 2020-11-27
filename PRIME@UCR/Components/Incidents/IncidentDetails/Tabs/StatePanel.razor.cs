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

using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists.InIncident;
using PRIME_UCR.Components.Incidents.IncidentDetails.Constants;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models.CheckLists;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class StatePanel
    {

        public string nextState;

        public List<Tuple<string, string>> PendingTasks = new List<Tuple<string, string>>();

        [Parameter]
        public IncidentDetailsModel Incident { get; set; }
        [Parameter] 
        public EventCallback OnSave { get; set; }
        [Parameter]
        public Persona CurrentUser { get; set; }
        [Inject]
        public IIncidentService IncidentService { get; set; }
        [Inject]
        private NavigationManager NavManager { get; set; }

        private int currentStateIndex;
        MatButton Button2;
        BaseMatMenu Menu2;
        protected string IncidentURL = "/incidents/";
        public string incidentcod { get; set; }
        public IMailService mailService { get; set; }
        public IAssignmentService AssignmentService { get; set; }
        private bool _isLoading = true;

        private AssignmentModel _model;

        private List<EspecialistaTécnicoMédico> _specialists;

        public IUserService userService { get; set; }

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
            Tuple.Create(IncidentStates.Reactivated.Nombre, "Reactivación"),
            Tuple.Create(IncidentStates.Done.Nombre, "Finalizado")
        };

        protected override async Task OnInitializedAsync()
        {
            await LoadValues();
        }

        public async Task LoadValues()
        {
            _isLoading = true;
            StateHasChanged();
            currentStateIndex = IncidentStatesList.FindIndex(i => i.Item1 == Incident.CurrentState);
            nextState = (await IncidentService.GetNextIncidentState(Incident.Code)).ToString();
            PendingTasks = await IncidentService.GetPendingTasksAsync(Incident, nextState);
            _isLoading = false;
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
            sendInformation();
            await IncidentService
                .ApproveIncidentAsync(Incident.Code, CurrentUser.Cédula);
            await OnSave.InvokeAsync(null);
            await LoadValues();
        }

        private async Task Reject()
        {
            await IncidentService
                .RejectIncidentAsync(Incident.Code, CurrentUser.Cédula);
            await OnSave.InvokeAsync(null);
            await LoadValues();
        }

        private async Task ChangeState()
        {
            await IncidentService.ChangeState(Incident.Code, nextState);
            await OnSave.InvokeAsync(null);
            await LoadValues();
        }

        public void RedirectToTab(string url)
        {
            NavManager.NavigateTo($"{"/incidents/"+Incident.Code+"/"+ url}", forceLoad: true);
        }

        public async void sendInformation()
        {
            _model = await AssignmentService.GetAssignmentsByIncidentIdAsync(Incident.Code);
            _specialists = _model.TeamMembers;
            /*_specialists =
                (await AssignmentService.GetSpecialistsAsync())
                .ToList();*/

            foreach (var special in _specialists)
            {
                var user = (await userService.GetAllUsersWithDetailsAsync()).ToList().Find(u => u.CedPersona == special.Cédula);
                var url = "https://localhost:44368" + IncidentURL + incidentcod;
                var message = new EmailContentModel()
                {
                    Destination = user.Email,
                    Subject = "PRIME@UCR: Asignado al incidente:" + incidentcod,
                    Body = $"<p>Proceda a completar las listas de chequeo asignadas al incidente:<a href=\"{url}\">Haga click aquí para ser redirigido</a></p>"
                };

                await mailService.SendEmailAsync(message);

                StateHasChanged();
            }
        }
    }
}


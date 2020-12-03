using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.DTOs.UserAdministration;
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
        private bool _isLoading = true;


        [Inject]
        public IMailService mailService { get; set; }

        [Inject]
        public IAssignmentService AssignmentService { get; set; }

        private List<EspecialistaTécnicoMédico> _specialists;

        
        [Inject]
        public IUserService userService { get; set; }

        private AssignmentModel _model;
        protected string IncidentURL = "/incidents/";

        private CoordinadorTécnicoMédico coordinators;

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
            sendInformation();
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
           
            if (nextState == "Asignado") {
                _model = await AssignmentService.GetAssignmentsByIncidentIdAsync(Incident.Code);
                _specialists = _model.TeamMembers;
                var allocator = CurrentUser.NombreCompleto;
                var url = "https://localhost:44368";
                foreach (var special in _specialists)
                {
                    var user = (await userService.GetAllUsersWithDetailsAsync()).ToList().Find(u => u.CedPersona == special.Cédula);
                    var message = new EmailContentModel()
                    {
                        Destination = user.Email,
                        Subject = "PRIME@UCR: Asignado al incidente:" + Incident.Code,
                        Body = $"<p>{allocator} lo ha asignado a un incidente</br>Proceda a completar las listas de chequeo asignadas al incidente:<a href=\"{url}\">Haga click aquí para ser redirigido</a></p>"
                    };
                    await mailService.SendEmailAsync(message);
                }
                coordinators = _model.Coordinator;
                var user2 = (await userService.GetAllUsersWithDetailsAsync()).ToList().Find(u => u.CedPersona == coordinators.Cédula);
                var message2 = new EmailContentModel()
                {
                    Destination = user2.Email,
                    Subject = "PRIME@UCR: Asignado al incidente:" + Incident.Code,
                    Body = $"<p>Usted ha sido asignado al incidente:{Incident.Code} por {allocator}. <a href=\"{url}\"> Haga click aquí para ser redirigido</a></p>"
                };
                await mailService.SendEmailAsync(message2);
            } 
        }

    }
}


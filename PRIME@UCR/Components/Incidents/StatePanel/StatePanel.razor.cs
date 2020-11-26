using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Components.Incidents.IncidentDetails.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Incidents.StatePanel
{
    public partial class StatePanel
    {
        [Parameter]
        public IncidentDetailsModel Incident { get; set; }
        [Parameter] 
        public EventCallback OnSave { get; set; }
        [Parameter]
        public Persona CurrentUser { get; set; }
        [Inject]
        public IIncidentService IncidentService { get; set; }
        [Inject]
        private IPersonService PersonService { get; set; }

        public string nextState;
        //Needed for StatePendingTasks component
        public List<Tuple<string, string>> PendingTasks = new List<Tuple<string, string>>();
        //Needed for StateLog component
        private List<Tuple<DateTime, string>> StatesTemporaryLog = new List<Tuple<DateTime, string>>();
        // The list to pass to State log
        private List<Tuple<DateTime, Persona>> StatesLog = new List<Tuple<DateTime, Persona>>();
        //List of incidents
        private readonly IncidentStatesList _states = new IncidentStatesList();
        public int currentStateIndex;
        MatButton Button2;
        BaseMatMenu Menu2;
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadValues();
        }

        public async Task LoadValues()
        {
            _isLoading = true;
            StateHasChanged();
            currentStateIndex = _states.List.FindIndex(i => i.Item1 == Incident.CurrentState);
            nextState = (await IncidentService.GetNextIncidentState(Incident.Code)).ToString();
            PendingTasks = await IncidentService.GetPendingTasksAsync(Incident, nextState);
            StatesTemporaryLog = await IncidentService.GetStatesLog(Incident.Code);
            await updateLog();
            _isLoading = false;
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
            Incident.Reviewer = CurrentUser;
            await IncidentService.ChangeState(Incident, nextState);
            await OnSave.InvokeAsync(null);
            await LoadValues();
        }

        /*This method will create a new log, but instead of only id will have the person itself*/
        private async Task updateLog()
        {
            StatesLog.Clear();
            foreach (var state in StatesTemporaryLog)
            {
                try
                {
                    var person = await PersonService.GetPersonByCedAsync(state.Item2);
                    StatesLog.Add(Tuple.Create(state.Item1, person));
                } catch(Exception e) {}
            }
        }
    }
}


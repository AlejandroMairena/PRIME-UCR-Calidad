
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
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
        [Parameter] 
        public LastChangeModel LastChange { get; set; }
        [Inject]
        public IIncidentService IncidentService { get; set; }
        [Inject]
        private IPersonService PersonService { get; set; }

        public string nextState;
        //Needed for StatePendingTasks component
        public List<Tuple<string, string>> PendingTasks = new List<Tuple<string, string>>();
        // The list to pass to State log
        private List<StatesModel> StatesLog = new List<StatesModel>();
        //List of incidents
        private readonly IncidentStatesList _states = new IncidentStatesList();
        public int currentStateIndex;
        MatButton Button2;
        BaseMatMenu Menu2;
        private bool _isLoading = true;
        // Arrays needed for SummaryMessage to show Last Change in Incident
        public List<string> Values = new List<string>();
        public List<string> Content = new List<string>();
        // Needed for feedback
        private IncidentFeedbackModel _feedBackmodel = new IncidentFeedbackModel();
        [CascadingParameter] public Pages.Incidents.IncidentDetails ParentPage { get; set; }

        public bool showFeedBack = false;

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(LastChange.UltimoCambio))
            {
                Content = new List<string> { "Responsable: ", "Fecha: ", "Modificación en: " };
                Values = new List<string> { LastChange.Responsable.NombreCompleto,
                                        LastChange.FechaHora.ToString(), LastChange.UltimoCambio };
            }
            await LoadValues();
        }

        public async Task LoadValues()
        {
            _isLoading = true;
            StateHasChanged();
            currentStateIndex = _states.List.FindIndex(i => i.Item1 == Incident.CurrentState);
            nextState = (await IncidentService.GetNextIncidentState(Incident.Code)).ToString();
            PendingTasks = await IncidentService.GetPendingTasksAsync(Incident, nextState);
            StatesLog = await IncidentService.GetStatesLog(Incident.Code);
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

        private void showFeedbackInput()
        {
            if (showFeedBack)
            {
                showFeedBack = false;
                _feedBackmodel.FeedBack = " ";
            }
            else
            {
                showFeedBack = true;
            }
        }
        
        private async Task Reject()
        {
            showFeedBack = false;
            await createFeedBack();
            await IncidentService
                .RejectIncidentAsync(Incident.Code, CurrentUser.Cédula);
            await OnSave.InvokeAsync(null);
            await LoadValues();
            ParentPage.refresh();
        }

        private async Task createFeedBack()
        {
            string Code = Incident.Code;
            string FeedBack = _feedBackmodel.FeedBack;
            await IncidentService.InsertFeedback(Code, FeedBack);
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
            foreach (var state in StatesLog)
            {
                try
                {
                    var person = await PersonService.GetPersonByIdAsync(state.AprobadoPor);
                    state.Aprobador = person;
                } catch(Exception e) {}
            }
        }
    }
}


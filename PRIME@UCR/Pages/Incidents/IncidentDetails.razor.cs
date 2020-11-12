using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Exceptions;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentDetails
    {
        const DetailsTab DefaultTab = DetailsTab.Info;
        
        [Parameter] public string Code { get; set; }
        [Parameter] public string StartingTab { get; set; }

        private bool _exists = true;

        private readonly List<Tuple<DetailsTab, string>> _tabs = new List<Tuple<DetailsTab, string>>();

        private DetailsTab _activeTab;
        private IncidentDetailsModel _incidentModel;
        private string _statusMessage = "";
        private string _statusClass = "";
        public Action ClearStatusMessageCallback { get; set; }
        public Action<DetailsTab> ChangeActiveTabCallback { get; set; }

        private void FillTabStates()
        {
            _tabs.Clear();
            var tabValues = Enum.GetValues(typeof(DetailsTab)).Cast<DetailsTab>();
            foreach (var tab in tabValues)
            {
                switch (tab)
                {
                    case DetailsTab.Info:
                        _tabs.Add(new Tuple<DetailsTab, string>(DetailsTab.Info, ""));
                        break;
                    case DetailsTab.Origin:
                        _tabs.Add(_incidentModel.Origin == null
                            ? new Tuple<DetailsTab, string>(DetailsTab.Origin, "warning")
                            : new Tuple<DetailsTab, string>(DetailsTab.Origin, ""));
                        break;
                    case DetailsTab.Destination:
                        _tabs.Add(_incidentModel.Destination == null
                            ? new Tuple<DetailsTab, string>(DetailsTab.Destination, "warning")
                            : new Tuple<DetailsTab, string>(DetailsTab.Destination, ""));
                        break;
                    case DetailsTab.Patient:
                        _tabs.Add(_incidentModel.MedicalRecord == null
                            ? new Tuple<DetailsTab, string>(DetailsTab.Patient, "warning")
                            : new Tuple<DetailsTab, string>(DetailsTab.Patient, ""));
                        break;
                    case DetailsTab.Assignment:
                        _tabs.Add(new Tuple<DetailsTab, string>(DetailsTab.Assignment, ""));
                        break;
                    case DetailsTab.Multimedia:
                        _tabs.Add(new Tuple<DetailsTab, string>(DetailsTab.Multimedia, ""));
                        break;
                }
            }
        }

        private DetailsTab GetTabByName(string tabName)
        {
            switch (tabName)
            {
                case "Assignment":
                    return DetailsTab.Assignment;
                default:
                    return DefaultTab;
            }
        }
        
        protected override void OnInitialized()
        {
            ClearStatusMessageCallback = ClearStatusMessage;
            ChangeActiveTabCallback = ChangeActiveTab;
            _activeTab = String.IsNullOrWhiteSpace(StartingTab)
                ? DefaultTab
                : GetTabByName(StartingTab);
        }

        protected override async Task OnInitializedAsync()
        {
            _incidentModel = await IncidentService.GetIncidentDetailsAsync(Code);
            if (_incidentModel == null)
                _exists = false;
            else
                FillTabStates(); 
        }

        private async Task Save(IncidentDetailsModel model)
        {
            try
            {
                _incidentModel = await IncidentService.UpdateIncidentDetailsAsync(model);
                _statusMessage = "Se guardaron los cambios exitosamente.";
                _statusClass = "success";
            }
            catch (DomainException e)
            {
                _incidentModel = await IncidentService.GetIncidentDetailsAsync(_incidentModel.Code);
                _statusMessage = e.Message;
                _statusClass = "danger";
            }
            FillTabStates();
        }

        private void ClearStatusMessage()
        {
            _statusMessage = "";
            StateHasChanged();
        }

        private void ChangeActiveTab(DetailsTab newTab)
        {
            _activeTab = newTab;
            StateHasChanged();
        }
    }
}
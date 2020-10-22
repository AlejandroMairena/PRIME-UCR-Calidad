using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentDetails
    {
        const DetailsTab DefaultTab = DetailsTab.Info;
        
        [Parameter]
        public string Code { get; set; }

        private bool _exists = true;

        private readonly List<Tuple<DetailsTab, string>> _tabs = new List<Tuple<DetailsTab, string>>();
        
        private DetailsTab _activeTab = DefaultTab;
        private IncidentDetailsModel _incidentModel;

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
                }
            }
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
            _incidentModel = await IncidentService.UpdateIncidentDetailsAsync(model);
            FillTabStates();
        }
    }
}
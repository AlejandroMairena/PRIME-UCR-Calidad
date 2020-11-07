using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using PRIME_UCR.Domain.Constants;
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
            IncidentStates.InCreationProcess.Nombre,
            IncidentStates.Created.Nombre,
            IncidentStates.Rejected.Nombre,
            IncidentStates.Approved.Nombre,
            IncidentStates.Assigned.Nombre,
            IncidentStates.Preparing.Nombre,
            IncidentStates.InOriginRoute.Nombre,
            IncidentStates.PatientInOrigin.Nombre,
            IncidentStates.InRoute.Nombre,
            IncidentStates.Delivered.Nombre,
            IncidentStates.Reactivated.Nombre,
            IncidentStates.Done.Nombre
        };
        MatButton Button2;
        BaseMatMenu Menu2;

        public void OnClick(MouseEventArgs e)
        {
            this.Menu2.OpenAsync(Button2.Ref);
        }
    }
}


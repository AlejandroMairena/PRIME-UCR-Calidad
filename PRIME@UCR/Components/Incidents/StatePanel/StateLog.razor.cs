using Microsoft.AspNetCore.Components;
using PRIME_UCR.Components.Incidents.IncidentDetails.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Incidents.StatePanel
{
    public partial class StateLog
    {
        [Parameter]
        public List<Tuple<DateTime, string>> StatesLog { get; set; }
        private IncidentStatesList States = new IncidentStatesList();
    }
}

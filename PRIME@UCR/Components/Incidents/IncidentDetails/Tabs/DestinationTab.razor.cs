using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class DestinationTab
    {
        private Ubicacion _destionation;

        void OnChangeDestination(Ubicacion location)
        {
            _destionation = location;
        }
    }
}

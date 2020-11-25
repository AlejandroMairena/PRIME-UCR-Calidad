using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsCounterComponent
    {
        [Parameter]
        public IncidentsCounterModel Value { get; set; }
        [Parameter]
        public EventCallback<IncidentsCounterModel> ValueChanged { get; set; }
    }
}

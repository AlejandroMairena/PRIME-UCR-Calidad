using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Components.Dashboard.IncidentsGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.Dashboard
{
    public partial class Dashboard
    {
        public FilterModel Value = new FilterModel();
        public bool _finishedLoadingCounters = false;


        //FILTER COMPONENT

        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        private async Task ClearFilters()
        {
            Value = new FilterModel();
            await ValueChanged.InvokeAsync(Value);
        }

        private async Task ApplyFilters()
        {
            await ValueChanged.InvokeAsync(Value);
        }


    }
}

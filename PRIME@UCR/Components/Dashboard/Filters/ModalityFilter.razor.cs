using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    public partial class ModalityFilter
    {
        [Inject]
        public IIncidentService IncidentService { get; set; }
        [Parameter]
        public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }

        private List<Modalidad> _modes;
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            _modes =
                (await IncidentService.GetTransportModesAsync())
                .ToList();
            _isLoading = false;
        }

    }
}

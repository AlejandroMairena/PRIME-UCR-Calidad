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
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        private List<Modalidad> _modes;
        private bool _isLoading = true;
        private bool _changesMade = false;
        protected override async Task OnInitializedAsync()
        {
            _modes =
                (await IncidentService.GetTransportModesAsync())
                .ToList();
            _isLoading = false;
        }

        private async Task OnModalityChange(Modalidad modalidad) 
        {
            if (modalidad == Value.ModalityFilter)
            {
                _changesMade = false;
            }
            else
            {
                _changesMade = true;
            }
            Value._selectedModality = modalidad;
            await ValueChanged.InvokeAsync(Value);
        }
        private async Task Discard()
        {
            _changesMade = false;
            Value._selectedModality = Value.ModalityFilter;
            await ValueChanged.InvokeAsync(Value);
        }
        private async Task Save()
        {
            StateHasChanged();
            Value.ModalityFilter = Value._selectedModality;
            if (Value.ModalityFilter != null)
            {
                Value.ButtonEnabled = true;
            }
            else
            {
                Value.ButtonEnabled = false;
            }
            _changesMade = false;
            await ValueChanged.InvokeAsync(Value);
        }

    }
}

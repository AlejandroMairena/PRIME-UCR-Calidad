using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class TransportUnitPicker
    {
        [Inject] public ILocationService LocationService { get; set; }
        [Parameter] public AssignmentModel Value { get; set; }
        [Parameter] public bool IsFirst { get; set; }
        [Parameter] public string Mode { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }
        [Parameter] public EventCallback<AssignmentModel> OnSave { get; set; }


        private UnidadDeTransporte _selectedTransportUnit { get; set; }
        private List<UnidadDeTransporte> _transportUnits;

        private bool _saveButtonEnabled;
        private EditContext _context;

        private async Task LoadTransportUnits(bool firstRender)
        {
            _transportUnits =
                (await LocationService.GetAllTransporUnitsByMode(Mode))
                .ToList();
            if (firstRender)
            {
                Value.TransportUnit = null;
            }
        }
 
        private async Task Submit()
        {
            await OnSave.InvokeAsync(Value);
            _context.OnFieldChanged -= HandleFieldChanged;
            _context = new EditContext(model: Value);
            _context.OnFieldChanged += HandleFieldChanged;
            _saveButtonEnabled = false;
        }
        private async Task Discard()
        {
            await OnDiscard.InvokeAsync(null);
            await LoadTransportUnits(true);
        }
        protected override async Task OnInitializedAsync()
        {
            if (IsFirst)
                Value = new AssignmentModel {};

            await LoadTransportUnits(true);
            _saveButtonEnabled = IsFirst;

            _context = new EditContext(model: Value);
            _context.OnFieldChanged += HandleFieldChanged;
        }

        // used to toggle submit button disabled attribute
        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            _saveButtonEnabled = _context.IsModified();
            StateHasChanged();
        }

        public void Dispose()
        {
            _context.OnFieldChanged -= HandleFieldChanged;
        }

    }
}

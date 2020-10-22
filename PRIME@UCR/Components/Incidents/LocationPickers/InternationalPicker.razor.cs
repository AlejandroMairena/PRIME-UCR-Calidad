using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{

    public partial class InternationalPicker
    {
        [Inject] public ILocationService LocationService { get; set; }
        [Parameter] public InternationalModel Value { get; set; }
        [Parameter] public EventCallback<InternationalModel> OnSave { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }
        [Parameter] public bool IsFirst { get; set; }
        
        private bool _saveButtonEnabled = false;
        private EditContext _context;
        private List<Pais> _values;

        private async Task Submit()
        {
            await OnSave.InvokeAsync(Value);
            _context.OnFieldChanged -= HandleFieldChanged;
            _context = new EditContext(Value);
            _context.OnFieldChanged += HandleFieldChanged;
            _saveButtonEnabled = false;
        }

        private async Task Discard()
        {
            await OnDiscard.InvokeAsync(null);
        }

        protected override async Task OnInitializedAsync()
        {
            _values = (await LocationService.GetAllCountriesAsync())
                .Where(c => c.Nombre != Pais.DefaultCountry)
                .ToList();
            
            if (IsFirst)
                Value = new InternationalModel();
            
            _saveButtonEnabled = IsFirst;
            
            _context = new EditContext(Value);
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

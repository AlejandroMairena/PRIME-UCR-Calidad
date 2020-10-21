using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{

    public partial class InternationalPicker
    {
        [Inject]
        public ILocationService LocationService { get; set; }
        
        [Parameter]
        public Ubicacion Value { get; set; }
        
        [Parameter]
        public EventCallback<Ubicacion> ValueChanged { get; set; }

        private List<Pais> _values;

        private Pais _country;
        
        async Task OnCountryChange(Pais country)
        {
            _country = country;
            await Callback();
        }

        async Task Callback()
        {
            Internacional intl = null;
            if (_country != null)
                intl = new Internacional
                {
                    NombrePais = _country.Nombre,
                };
            await ValueChanged.InvokeAsync(intl);
        }

        public async Task LoadExistingValues()
        {
            if (Value is Internacional intl)
            {
                // get strongly typed obj instead of only having FK
                _country = _values.First(p => p.Nombre == intl.NombrePais);
            }
            else
            {
                _country = null;
                await Callback();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _values = (await LocationService.GetAllCountriesAsync())
                .Where(c => c.Nombre != Pais.DefaultCountry)
                .ToList();
            
            await LoadExistingValues();
        }
    }
}

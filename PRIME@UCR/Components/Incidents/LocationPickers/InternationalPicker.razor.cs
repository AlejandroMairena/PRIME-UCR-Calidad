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

        private Internacional _international;
        
        async Task OnCountryChange(Pais country)
        {
            _international = new Internacional()
            {
                NombrePais = country.Nombre,
            };
            await ValueChanged.InvokeAsync(_international);
        }

        protected override async Task OnInitializedAsync()
        {
            _values = (await LocationService.GetAllCountriesAsync())
                .Where(c => c.Nombre != Pais.DefaultCountry)
                .ToList();
            
            if (Value is Internacional intl)
            {
                _international = intl;
                // get strongly typed obj instead of only having FK
                _international.Pais = _values.First(p => p.Nombre == intl.NombrePais);
            }
            else
            {
                var country = _values.First();
                _international = new Internacional
                {
                    NombrePais = country.Nombre,
                    Pais = country
                };
            }
            
            await ValueChanged.InvokeAsync(_international);
        }
    }
}

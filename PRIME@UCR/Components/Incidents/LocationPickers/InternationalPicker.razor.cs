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
        public EventCallback<Ubicacion> OnChange { get; set; }

        private List<Tuple<Pais, string>> _values;

        private Internacional _international;
        
        async Task OnCountryChange(Pais country)
        {
            _international = new Internacional()
            {
                NombrePais = country.Nombre,
            };
            await OnChange.InvokeAsync(_international);
        }

        protected override async Task OnInitializedAsync()
        {
            var values = (await LocationService.GetAllCountriesAsync())
                .ToList();
            _values = DropDownUtilities.LoadAsTupleList(values, "Nombre");
            
            var country = values.First();
            _international = new Internacional { NombrePais = country.Nombre };
            await OnChange.InvokeAsync(_international);
        }
    }
}

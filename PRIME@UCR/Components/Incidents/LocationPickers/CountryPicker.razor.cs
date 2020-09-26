using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;


namespace PRIME_UCR.Components.Incidents.LocationPickers
{

    public partial class CountryPicker
    {
        [Inject]
        public IIncidentService IncidentService { get; set; }

        private List<Tuple<Pais, string>> _values;

        async Task UpdateList()
        {
            _values = (await IncidentService.GetAllCountriesAsync())
                .Select(c => Tuple.Create(c, c.Nombre))
                .ToList();
        }



    }
}

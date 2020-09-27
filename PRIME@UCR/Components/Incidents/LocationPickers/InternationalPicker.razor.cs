using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Models.Incidents;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{

    public partial class InternationalPicker
    {
        [Inject]
        public ILocationService LocationService { get; set; }

        private List<Tuple<Pais, string>> _values;

        [Parameter]
        public InternationalOriginModel Model { get; set; }

        async Task InitializeComponent()
        {
            _values = (await LocationService.GetAllCountriesAsync())
                .Select(c => Tuple.Create(c, c.Nombre))
                .ToList();
            var tuple = _values.FirstOrDefault();
            if (tuple != null)
                Model = new InternationalOriginModel { Country = tuple.Item1 };
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class ProvincePicker
    {
        [Inject]
        public IIncidentService IncidentService { get; set; }

        private List<Tuple<Provincia, string>> _values;

        async Task UpdateList()
        {
            _values = (await IncidentService.GetAllProvincesAsync())
                .Select(p => Tuple.Create(p, p.Nombre))
                .ToList();
        }

    }
}

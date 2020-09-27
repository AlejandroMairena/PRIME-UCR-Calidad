using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class MedicalCenterPicker
    {
        [Inject]
        public IIncidentService IncidentService { get; set; }

        private List<Tuple<CentroMedico, string>> _values;

        async Task UpdateList()
        {
            _values = (await IncidentService.GetAllMedicalCentersAsync())
                .Select(mc => Tuple.Create(mc, mc.Nombre))
                .ToList();
        }
    }
}
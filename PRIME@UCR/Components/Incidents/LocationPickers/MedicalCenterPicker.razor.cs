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
        public ILocationService LocationService { get; set; }
        
        [Parameter]
        public EventCallback<Ubicacion> OnChange { get; set; }
        
        private CentroMedico _selectedMedicalCenter;

        private List<Tuple<CentroMedico, string>> _values;

        async Task Callback()
        {
            var location = new CentroUbicacion
            {
                CentroMedicoId = _selectedMedicalCenter.Id
            };
            await OnChange.InvokeAsync(location);
        }

        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            _selectedMedicalCenter = medicalCenter;

            await Callback();
        }
        
        protected override async Task OnInitializedAsync()
        {
            var medicalCenters =
                (await LocationService.GetAllMedicalCentersAsync())
                .ToList();
            
            _values = medicalCenters
                .Select(mc => Tuple.Create(mc, mc.Nombre))
                .ToList();
            
            _selectedMedicalCenter = medicalCenters.First();

            await Callback();
        }
    }
}
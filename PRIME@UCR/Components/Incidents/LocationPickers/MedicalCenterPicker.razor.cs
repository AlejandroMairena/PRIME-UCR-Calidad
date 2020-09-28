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
        public Ubicacion Value { get; set; }
        
        [Parameter]
        public EventCallback<Ubicacion> ValueChanged { get; set; }
        
        private CentroMedico _selectedMedicalCenter;

        private List<CentroMedico> _values;

        async Task Callback()
        {
            var location = new CentroUbicacion
            {
                CentroMedicoId = _selectedMedicalCenter.Id
            };
            await ValueChanged.InvokeAsync(location);
        }

        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            _selectedMedicalCenter = medicalCenter;

            await Callback();
        }
        
        protected override async Task OnInitializedAsync()
        {
            _values =
                (await LocationService.GetAllMedicalCentersAsync())
                .ToList();

            if (Value is CentroUbicacion location)
            {
                _selectedMedicalCenter = _values.First(mc => mc.Id == location.CentroMedicoId);
            }
            else
            {
                _selectedMedicalCenter = _values.First();
                await Callback();
            }

        }
    }
}